using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class Bog
    {
        private string _titel;
        private string _forfatter;
        private int _sidetal;
        private string _isbn13;



        public string Titel
        {
            get { return _titel; }
            set { CheckTitel(value); _titel = value; }
        }

        public string Forfatter
        {
            get { return _forfatter; }
            set { CheckForfatter(value); _forfatter = value; }
        }

        public int Sidetal
        {
            get { return _sidetal; }
            set { CheckSidetal(value); _sidetal = value; }
        }

        public string ISBN13
        {
            get { return _isbn13; }
            set { CheckISBN13(value); _isbn13 = value; }
        }


        //constructors
        public Bog() { }

        public Bog(string titel, string forfatter, int sidetal, string isbn13)
        {

            CheckTitel(titel);
            CheckForfatter(forfatter);
            CheckSidetal(sidetal);
            CheckISBN13(isbn13);
            _titel = titel;
            _forfatter = forfatter;
            _sidetal = sidetal;
            _isbn13 = isbn13;

        }




        //Checks
        private static void CheckTitel(string titel)
        {
            if (string.IsNullOrWhiteSpace(titel) || titel.Length < 2)
            {
                throw new ArgumentException("Titel skal være længere end 2 tegn og være Gyldig");
            }
        }

        private static void CheckForfatter(string forfatter)
        {
            if (string.IsNullOrWhiteSpace(forfatter))
            {
                throw new ArgumentException("Forfatter er ikke gyldig eller tom");
            }
        }

        private static void CheckSidetal(int sidetal)
        {
            if (sidetal < 10 || sidetal > 1000)
            {
                throw new ArgumentOutOfRangeException("Sidetal skal være mellem 10 og 1000 sider");
            }
        }

        private static void CheckISBN13(string isbn13)
        {
            if (isbn13.Length < 13 || isbn13.Length > 13)
            {
                throw new ArgumentOutOfRangeException("ISBN13 Skal være præcist 13 karakterer langt");
            }
        }

        //tilføjet tostring metode
        public override string ToString()
        {
            return $"{nameof(Titel)}: {Titel}, {nameof(Forfatter)}: {Forfatter}, {nameof(Sidetal)}: {Sidetal}, {nameof(ISBN13)}: {ISBN13}";
        }

    }
}
