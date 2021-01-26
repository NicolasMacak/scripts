using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public class interactObjectNames
    {
        public const string SYRINGE = "Syringe";
        public const string BOTTLE = "Bottle";
        public const string PHONE = "Phone";

        public const string SyringeOltar = SYRINGE + nameSpacesStrings.Oltar;
        public const string PhoneOltar = PHONE + nameSpacesStrings.Oltar;
        public const string BottleOltar = BOTTLE + nameSpacesStrings.Oltar;

        public const string SyringeEnabler = SYRINGE + nameSpacesStrings.Enabler;
        public const string PhoneEnabler = PHONE + nameSpacesStrings.Enabler;
        public const string BottleEnabler = BOTTLE + nameSpacesStrings.Enabler;

        public const string SyringeCard = SYRINGE + nameSpacesStrings.Card;
        public const string PhoneCard = PHONE + nameSpacesStrings.Card;
        public const string BottleCard = BOTTLE + nameSpacesStrings.Card;

        public const string NOTEMission = "Mission";
        public const string NOTENakabot = "NakabotInfo";
        public const string NOTEBottle = "BottleInfo";
        public const string NOTESyringe = "SyringeInfo";
        public const string NOTEPhone = "PhoneInfo";
    }
    public class textSuffixes
    {
        public const string PICK_UP = " Zobrat(E)";
        public const string USE = " Pouzit (E)";
        public const string READ = " Precitat (E)";
    }

    public class nameSpacesStrings
    {
        public const string Oltar = "Oltar";
        public const string Enabler = "Enabler";
        public const string Card = "Card";
        public const string CheckPoint = "CheckPoint";

        public const string Lasers = "Lasers";
    }

    public class Notes
    {
        public const string Mission = "Tuto poznamku ti nechavam, ak by si sa sem nakoniec dostal. @" +
            "Oficialne znamy vznik korony je medializovana loz. Pravdou je, ze pred 10 rokmi sa tu objavil mocny artefakt prostrednictvom ktoreho sa tato choroba zacala sirit. @" +
            "Snazili sme sa proti nej bojovat, ale velmi dlho sme nezaznamenali serioznejsi progress. @ @" +
            "Avsak, neskor sa nam podarilo najst 3 objekty z ktorych artefakt cerpa moc.@ @" +
            "Postavili sme tuto stanicu a zacali hladat sposob ako artefakty znicit. Zostrojili sme 3 desturktory. pre kazdy predmet jeden.@" +
            "Artefakt sme ale podcenili, skor nez sme destruktory stihli pouzit, prevzal kontrolu nad obrannymi zlozkami zakladne a zlikvidoval nas.@ @" +
            "Musis najst tela mojich kolegov, a zobrat im karty s ktorymi mas pristup k objektom moci. @@ Je na tebe dokoncit co sme zacali.";

        public const string Nakabot = "NakaBot poslednej generacie @" +
            " V Kill Mode vie uspesne trvalo neutralizovat kazdu hrozbu ktora je dost odvazna na to aby prisla do jeho bezprostrednej blizkosti. @" +
            "14.2.2030 @" +
            " SAVka nam stale nedoniesla nahradne zvuko prijimace. Kym sa tak nestane, nemozem tomuto fesakovi zapnut spracovavanie zvukov z okolia." +
            "Je prakticky hluchy. @ @" +
            "17.2.2030 @" +
            "Dnes som ho videl vymykat sa protokolom. Aj ked neslo o nic vazne, rusim mu jeho opravnenia na pristup do miestnosti kde su uchovavane objekty moci. @" +
            "Ak sa nieco stane, tam sme v bezpeci.";

        public const string Syringe = "Zahodena vakcina @ " +
            " Tento kusok sa do nasho repertoaru pridal uplne kakoniec. @ Zaznamy hovoria ze jeden obcan isiel na vakcinaciu len kvoli tomu" +
            " aby vyvolal skandal a znicil tolko kuskov vakcin kolko bude moct. Nakaboti na mieste ho velmi rychlo spacifikovali, a jeho pokus bol neuspesny. @@" +
            "Po tomto akte sme na zariadeniach zaznamenali extremne vysoke hodnoty." +
            "@ @Doviedli nas k tejto striekacke co bola hodena na zemi";


        public const string Bottle = "Vodka s Bromhexinom @" +
            "V dnesnych casoch uz 2 krat v base sa vyznavanie extremizmu. V casoch minulych to bol politik, ktory si dostal dost hlasov na to aby sa dostal do parlamentu. Maly zazrak. @" +
            "Dlhy cas tvrdil ze korona neexistuje a po tom co ju sam dostal sa z neho stal chodiaci oxymoron. @@" +
            "Nasiel si na boj proti nej vlastne prostriedky. Boli rovnako uzitocne ako zmyslaci organ tohto jedinca @@ " +
            "Kratko po namixovani svojho lieku nam nase meradla zacali intenzivne hlasit polohu dalsieho objketu." +
            "@@ Predmet bol kratko nato najdeny v dome tohto jedinca.";


        public const string Phone = "Dezinformacny mobil @ " +
            "Prvy objekt moci ktory sa nam podarilo najst. Kym sme ho zabezpecili, ludia nebezpecni pre seba a svoje okolie z neho cerpali " +
            "falosne informacie. Situacia bola o dost horsia ked tieto informacie zacali sirit. @@" +
            "Masy ludi zacali verit na svetove spiknutia, existencie sedych eminencii a tiez sa u nich vyvynula paranoja z toho ze budu sledovani" +
            " technologiami ktore v tej dobe este ani neexistovali. @" +
            "Nase bojo s touto pliagou sa o to zhorsili. @@" + 
            "Najnebezpecnejsi objekt moci.";
    }
}

