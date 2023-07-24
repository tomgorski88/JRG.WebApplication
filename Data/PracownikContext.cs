using Microsoft.EntityFrameworkCore;
using JRG.WebApplication.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;

namespace JRG.WebApplication.Data;

public class PracownikContext: DbContext
{
    public DbSet<Zmiana> Zmiany => Set<Zmiana>();
    public DbSet<Pracownik> Pracownicy => Set<Pracownik>();
	public DbSet<Stopien> Stopnie => Set<Stopien>();
    public DbSet<Prawko> Prawko => Set<Prawko>();
	public DbSet<Uprawnienie> Uprawnienia => Set<Uprawnienie>();
	public DbSet<UkonczonyKurs> UkonczoneKursy => Set<UkonczonyKurs>();
	public DbSet<Kurs> Kursy => Set<Kurs>();
    

    public string DbPath { get; set; }

    public PracownikContext()
	{
		var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
		DbPath = Path.Join(path, "baza.db");

	}

	protected override void OnConfiguring(DbContextOptionsBuilder options) =>
		options.UseSqlite($"Data Source={DbPath}");

	public void SeedInitialData()
	{

        if (Pracownicy.Any())
		{
			Pracownicy.RemoveRange(Pracownicy);
			SaveChanges();
		}

        if (Stopnie.Any())
        {
            Stopnie.RemoveRange(Stopnie);
            SaveChanges();
        }

        Zmiany.Add(new Zmiana() { Name = "Zmiana 1" });
        Zmiany.Add(new Zmiana() { Name = "Zmiana 2" });
        Zmiany.Add(new Zmiana() { Name = "Zmiana 3" });


        string[] stopnie = {"generał brygadier", "nadbrygadier", "starszy brygadier", "brygadier", "młodszy brygadier", "starszy kapitan", "kapitan", "młodszy kapitan", "aspirant sztabowy", "starszy aspirant", "aspirant", "młodszy aspirant", "starszy ogniomistrz", "ogniomistrz", "młodszy ogniomistrz", "starszy sekcyjny","sekcyjny", "starszy strażak", "strażak"};


        string[] kursy =
        {
            "Kurs chemiczny/SA PSP/SGSP", "Szkolenie CBRNE", "Szkolenie ADR", "Szkolenie TTN",
            "Szkolenie obsługa pojazdów", "Ochrona Radiologiczna", "HDS", "Napełnianie zbiorników ciśnieniowych",
            "Drabiny", "Podnosniki  w tym 2Ż", "Rzeki szybko płynące", "Ratownik medyczny",
            "Uprawnienia elektr. do 1kV", "Stermotorzysta", "Maski ODO", "Pilarz",
            "Instruktor poszukiwawczo-ratowniczy", "Kurs ratownictwa na terenach wodnych",
            "Instruktor ratownictwa technicznego", "Instruktor pożary wewnętrzne", "Instruktor ratownictwa chemicznego",
            "Kurs ratownictwa lodowego", "Instruktor ratownictwa wysokościowego"
        };


        for (int i = 0; i < stopnie.Length; i++)
        {
            Stopnie.Add(new Stopien{Name = stopnie[i] });
        }

        for (int i = 0; i < kursy.Length; i++)
        {
            Kursy.Add(new Kurs { Nazwa = kursy[i] });
        }

        var stopien = new Stopien { Name = "Brak" };
        Stopnie.Add(stopien);

        string[] pracownicy = { "Konrad Banaś", "Józef Bednarz", "Mateusz Bielaszka", "Marcin Bieniek", "Tomasz Bieniek", "Piotr Cebulak", "Tomasz Cieśla", "Michał Czyrek", "Krzysztof Drzał", "Mariusz Drzał", "Marcin Gałka", "Andrzej Gaweł", "Marcin Górniak", "Tomasz Górski", "Marcin Górski", "Mariusz Grądziel", "Mateusz Jachowicz", "Franciszek Jakubowski", "Mateusz Janda", "Maksymilian Kaszyca", "Hubert Kacała", "Tomasz Kliś", "Bartosz Kłos", "Grzegorz Kwolek", "Grzegorz Kot", "Patryk Kosturek ", "Mateusz Krok", "Grzegorz Krzak", "Łukasz Kusztyb", "Tomasz Kwasek", "Grzegorz Kulon", "Krzysztof Lignowski", "Karol Lech", "Bartłomiej Jagusiak", "Piotr Misiak", "Mirosław Peszko", "Krzysztof Plebanek", "Marcin Porada", "Rafal Prędki", "Paweł Pukała", "Daniel Pytel", "Sławomir Ryś",  "Robert Ryzner ", "Daniel Skowronek", "Roman Sport", "Mariusz Surowiec", "Marcin Telega", "Marek Tendaj", "Karol Wantrych", "Michał Wilkos",  "Paweł Kuszewski", "Janusz Pukała", "Sylwester Czosnek", "Miłosz Szczęścikiewicz", "Paweł Ruman", "Karol Lech",
        };

        for (int i = 0; i < pracownicy.Length; i++)
        {
            Pracownicy.Add(new Pracownik{ Nazwa = pracownicy[i], Adres = "Brak", DataUrodzenia = DateOnly.FromDateTime(DateTime.Today), DataZatrudnienia = DateOnly.FromDateTime(DateTime.Today), Notatka = "Brak", Telefon = "Brak", Stopien = stopien});
        }
        //
        var prawko1 = new Prawko { Nazwa = "A" };
        var prawko2 = new Prawko { Nazwa = "B" };
        var prawko3 = new Prawko { Nazwa = "C" };
        var prawko4 = new Prawko { Nazwa = "CE" };
        var prawko5 = new Prawko { Nazwa = "D" };
        var prawko6 = new Prawko { Nazwa = "DE" };
        var prawko7 = new Prawko { Nazwa = "E" };
        var prawko8 = new Prawko { Nazwa = "T" };

        Prawko.Add(prawko1);
        Prawko.Add(prawko2);
        Prawko.Add(prawko3);
        Prawko.Add(prawko4);
        Prawko.Add(prawko5);
        Prawko.Add(prawko6);
        Prawko.Add(prawko7);
        Prawko.Add(prawko8);


        SaveChanges();
	}
	
}
