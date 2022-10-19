using System.Security.Cryptography;

namespace ShopeeFood.Services
{
	public static class VerifyServices
	{
		private const int _saltSize = 16;
		private const int _keySize = 32;
		private const int _iterations = 100000;
		private static readonly HashAlgorithmName _algorithmName = HashAlgorithmName.SHA256;
		private const char segmentDelimiter = ':';

		public static string Hash(string secret)
		{
			var salt = RandomNumberGenerator.GetBytes(_saltSize);
			var key = Rfc2898DeriveBytes.Pbkdf2(secret, salt, _iterations, _algorithmName, _keySize);
			return String.Join(segmentDelimiter,
				Convert.ToHexString(key),
				Convert.ToHexString(salt),
				_iterations,
				_algorithmName
			);
		}

		public static bool Verify(string secret, string hash)
		{
			var segments = hash.Split(segmentDelimiter);
			var key = Convert.FromHexString(segments[0]);
			var salt = Convert.FromHexString(segments[1]);
			var iterations = int.Parse(segments[2]);
			var algorithm = new HashAlgorithmName(segments[3]);
			var inputSecretKey = Rfc2898DeriveBytes.Pbkdf2(
				secret,
				salt,
				iterations,
				algorithm,
				key.Length
			);
			return key.SequenceEqual(inputSecretKey);
		}

	}
}
