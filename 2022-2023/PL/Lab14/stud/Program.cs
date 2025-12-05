using System.Globalization;

namespace p3z
{
	class Program
	{
		static void Main(string[] args)
		{
			CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
			MovieDatabase db = new MovieDatabase();

			db.PartA();
			db.PartB();
			db.PartC();
			db.PartD();
			db.PartE();
		}
	}
}