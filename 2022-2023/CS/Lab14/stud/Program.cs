using System.Globalization;

namespace p3a
{
    class Program
    {
		static void Main(string[] args)
		{
			CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
			MovieDatabase db = new MovieDatabase();

			db.StageA();
			db.StageB();
			db.StageC();
			db.StageD();
			db.StageE();
		}
    }
}
