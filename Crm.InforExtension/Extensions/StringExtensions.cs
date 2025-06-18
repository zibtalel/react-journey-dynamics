namespace Crm.InforExtension.Extensions
{
	using System;
	using System.Globalization;
	using System.Linq;
	using System.Text;

	public static class StringExtensions
	{
		public static string NormalizeForInfor(this string input, int maxLength)
		{
			if (string.IsNullOrEmpty(input))
			{
				return input;
			}

			var stripped = input.RemoveDiacritics();
			var inforAllowedCharacters = new[]
				{
					'@', '[', ']', '\\', '^', '_', '`', '´', '`', '{', '}', '|', '°', '!', '"', '§', '$', '%', '&', '/', '(', ')', '=', '?',
					'²', '³', '<', '>', ',', '.', ';', ':', '-', '@', '€', '*', '+', '\'', '#', '~', ' ',
					'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
					'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
					'À', 'Á', 'Â', 'Ã', 'Ä', 'Å', 'Æ', 'Ç', 'È', 'É', 'Ê', 'Ë', 'Ì', 'Í', 'Î', 'Ï',
					'Ð', 'Ñ', 'Ò', 'Ó', 'Ô', 'Õ', 'Ö', '×', 'Ø', 'Ù', 'Ú', 'Û', 'Ü', 'Ý', 'Þ', 'ß',
					'à', 'á', 'â', 'ã', 'ä', 'å', 'æ', 'ç', 'è', 'é', 'ê', 'ë', 'ì', 'í', 'î', 'ï',
					'ð', 'ñ', 'ò', 'ó', 'ô', 'õ', 'ö', '÷', 'ø', 'ù', 'ú', 'û', 'ü', 'ý', 'þ', 'ÿ',
					'1', '2', '3', '4', '5', '6', '7', '8', '9', '0'
				};

			if (stripped.Length != input.Length)
			{
				throw new InvalidOperationException(string.Format("The strings are not of the same size: Infor character stripping doesn't work. Original: {0}, Stripped {1}", input, stripped));
			}
			var output = new StringBuilder();
			for (var i = 0; i < input.Length; i++)
			{
				output.Append(inforAllowedCharacters.Contains(input[i]) ? input[i] : stripped[i]);
			}

			return output.ToString().Substring(0, Math.Min(maxLength, output.ToString().Length));
		}
		public static string RemoveDiacritics(this string input)
		{
			var stFormD = input.Normalize(NormalizationForm.FormD);
			var sb = new StringBuilder();

			foreach (char t in stFormD)
			{
				var uc = CharUnicodeInfo.GetUnicodeCategory(t);
				if (uc != UnicodeCategory.NonSpacingMark)
				{
					sb.Append(t);
				}
			}

			return (sb.ToString().Normalize(NormalizationForm.FormC));
		}
	}
}