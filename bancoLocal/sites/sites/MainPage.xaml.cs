using SQLite;
using System.Collections.Specialized;
using System.Globalization;

namespace sites;

public partial class MainPage : ContentPage
{
    string dbPath; //indica onde esta o banco de dados
    SQLiteConnection conn;//representa a conexao com banco de dados

    public MainPage()
    {
        InitializeComponent();
    }

    public void ListarSites()
    {
        List<Site> lista = conn.Table<Site>().ToList();
        ListaClv.ItemsSource = lista;
    }



    private void CriarBD_Clicked(object sender, EventArgs e)
    {
		dbPath = System.IO.Path.Combine(FileSystem.AppDataDirectory, "sites.db3");
		conn = new SQLiteConnection(dbPath);
		conn.CreateTable<Site>();
        itensvsl.IsVisible = true;
        ListarSites();
    }


    private void btInserir_Clicked(object sender, EventArgs e)
    {
        //pegar dados da tela
        Site site = new Site();
        site.endereco = siteEnt.Text;

        //realizar as operacoes

        try
        {
            conn.Insert(site);
            //exibir o resultado da atividade
            //limpar os campos

            siteEnt.Text = "";
            idEnt.Text = "";

            //exibir uma mensagem cadastro realizado

            DisplayAlert("Sucesso", "Cadastro realizado com sucesso.", "OK");
        }
        catch
        {
            DisplayAlert("Falha", "Site ja Cadastrado!", "OK");
        }
        ListarSites();

    }

    private void btAlterar_Clicked(object sender, EventArgs e)
    {
       Site site = new Site();
        site.Id = Convert.ToInt32(idEnt.Text);
        site.endereco = siteEnt.Text;

        conn.Update(site);
        siteEnt.Text = "";
        idEnt.Text = "";

        DisplayAlert("Sucesso", "Cadastro Alterado com sucesso.", "OK");
        ListarSites();
    }

    private void btExcluir_Clicked(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(idEnt.Text);
        conn.Delete<Site>(id);

        siteEnt.Text = "";
        idEnt.Text = "";

        DisplayAlert("Sucesso", "Cadastro Deletado com sucesso.", "OK");
        ListarSites();
    }

    private void btLocalizar_Clicked(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(idEnt.Text);
        string nome = siteEnt.Text;

        var sites = from s in conn.Table<Site>()
                    where s.Id == id
                    select s;

        Site site = sites.First();
        idEnt.Text = site.Id.ToString();
        siteEnt.Text = site.endereco;
        ListarSites();

    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        var label = sender as Label;

        if (label != null && label.BindingContext is Site item)
        {
            idEnt.Text = item.Id.ToString();
            siteEnt.Text = item.endereco;
        }
    }

    private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
    {
        var label = sender as Label;

        if (label != null && label.BindingContext is Site item)
        {
           Launcher.OpenAsync(new Uri(item.endereco));
        }
    }
}

