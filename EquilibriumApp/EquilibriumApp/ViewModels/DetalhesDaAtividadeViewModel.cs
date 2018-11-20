using EquilibriumApp.Classes;
using EquilibriumApp.Models;
using Firebase.Database.Query;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EquilibriumApp.ViewModels
{
    public class DetalhesDaAtividadeViewModel : BaseViewModel
    {
        public DetalhesDaAtividadeViewModel()
        {
            Atv = new Atividade();
        }

        public Atividade Atv { get; set; }

        private bool alterado = false;
        private int avaliacao = 1;
        public int Avaliacao
        {
            get
            {
                return avaliacao;
            }
            set
            {
                SetProperty(ref avaliacao, value, nameof(Avaliacao));
                UpdateInFirebase();
                alterado = true;
            }
        }


        public ICommand ClickCommand => new Command<string>((url) =>
        {
            Device.OpenUri(new System.Uri(url));
        });

        public override async Task InitializeAsync(object navigationData)
        {
            Atv = navigationData as Atividade;
            if (Atv.Comentarios == null)
                Atv.Comentarios = new ObservableRangeCollection<Comment>();
            SetFirebase();
            try
            {
                var result = await Firebase.Child("comments").Child(Atv.Id).OnceAsync<Comment>();
                foreach (var i in result)
                {
                    Atv.Comentarios.Add(new Comment()
                    {
                        IdComment = i.Key,
                        Description = i.Object.Description,
                        IdUser = i.Object.IdUser,
                        UserName = i.Object.UserName,
                        IdRecommendedActivity = i.Object.IdRecommendedActivity
                    });
                }
                try
                {
                    var grade = await Firebase.Child("ratings").Child(Atv.Id).OrderByKey().EqualTo(SettingsService.IdUserAtual).LimitToFirst(1).OnceAsync<Ratting>();

                    if (grade.Count != 0)
                    {
                        foreach (var i in grade)
                            Avaliacao = i.Object.grade;
                    }
                    else
                    {
                        Avaliacao = 0;
                        alterado = false;
                    }
                }
                catch(Exception Ex)
                {

                }


            }
            catch(Exception Ex)
            {
                await DialogService.ShowMessage("Erro ao detalhar atividade. Tente novamente mais tarde","Erro");
            }
            finally
            {
                OnPropertyChanged(nameof(Atv));
                OnPropertyChanged(nameof(Atv.Comentarios));
                OnPropertyChanged(nameof(Atv.ReportsCount));
            }
            await base.InitializeAsync(navigationData);
        }

        public async void UpdateInFirebase()
        {
            if(alterado)
            {

                try
                {
                    var result = await Firebase.Child("ratings").Child(Atv.Id).OrderByKey().EqualTo(SettingsService.IdUserAtual).LimitToFirst(1).OnceAsync<Ratting>();

                    if (result.Count != 0)
                    {
                        await Firebase.Child("ratings").Child(Atv.Id).Child(SettingsService.IdUserAtual).PatchAsync(new Ratting(){ grade = avaliacao });
                    }
                    else
                    {
                        await Firebase.Child("ratings").Child(Atv.Id).Child(SettingsService.IdUserAtual).PatchAsync(new Ratting() { grade = avaliacao });
                    }
                    var grades = await Firebase.Child("ratings").Child(Atv.Id).OnceAsync<Ratting>();

                    int sum = 0;
                    foreach(var i in grades)
                    {
                        sum += i.Object.grade;
                    }

                    double rateFix = sum / grades.Count;

                    Atv.ReportsCount = rateFix;

                    OnPropertyChanged(nameof(Atv.ReportsCount));

                    await Firebase.Child("recommendedactivities").Child(Atv.Id).Child("averageRating").PutAsync(rateFix);
                }
                catch (Exception Ex)
                {
                    await DialogService.ShowMessage("Erro ao avaliar atividade", "Erro");
                }
            }
        }

    }
}