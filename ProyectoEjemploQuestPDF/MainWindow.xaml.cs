using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProyectoEjemploQuestPDF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerarPDF(object sender, RoutedEventArgs e)
        {
            //String ruta = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\imagen.jpg";
            String ruta = "./Basura";

            if (!File.Exists(ruta))
            {
                Directory.CreateDirectory(ruta);
            }

            ruta += "/imagen.jpg";
            Document
                .Create(document =>
                {
                    document.Page(page =>
                    {
                        page.Margin(1, Unit.Inch);

                        page.Header()
                            .AlignCenter()
                            .Text(Titulo.Text)
                            .FontSize(48)
                            .SemiBold();

                        page.Content()
                            .Row( row => 
                            {
                                row.RelativeItem()
                                .Image("../../assets/twitter.png");

                                row.RelativeItem()
                                    .Text("Jose Alfredo");
                            });

                        page.Content()
                            .Column( column =>
                            {
                                column.Spacing(0.5f, Unit.Inch);

                                using (WebClient client = new WebClient())
                                {
                                    client.DownloadFile(new Uri("https://fototrending.com/wp-content/uploads/fotografia-minimalista.jpg"), ruta);
                                }

                                column.Item()
                                    .Image(ruta);

                                column.Item()
                                    .Text(Placeholders.LoremIpsum() + Placeholders.LoremIpsum())
                                    .FontSize(18);
                            });
                        page.Footer()
                            .AlignCenter()
                            .Text(text =>
                           {
                               text.DefaultTextStyle(x => x.FontSize(10));

                               text.CurrentPageNumber();
                               text.Span(" / ");
                               text.TotalPages();
                           });
                    });
                }).GeneratePdf("Documento.pdf");
        }
    }
}
