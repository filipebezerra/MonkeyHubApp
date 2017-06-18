using MonkeyHubApp.Backend.DataObjects;
using System.Collections.Generic;
using System.Data.Entity;

namespace MonkeyHubApp.Backend.Models
{
    public class MonkeyHubContextInitializer : DropCreateDatabaseIfModelChanges<MonkeyHubAppContext>
    {
        protected override void Seed(MonkeyHubAppContext context)
        {
            base.Seed(context);

            List<Tag> tags = new List<Tag>()
            {
                new Tag()
                {
                    Name = "Eventos",
                    Slug = "eventos",
                    Description = "Eventos da comunidade Xamarin no Brasil"
                },
                new Tag()
                {
                    Name = "Xamarin Android",
                    Slug = "xamarin-android",
                    Description = "Aqui você vai encontrar ajuda relacionada a Xamarin.Android"
                },
                new Tag()
                {
                    Name = "Xamarin iOS",
                    Slug = "xamarin-ios",
                    Description = "Aqui você vai encontrar ajuda relacionada a Xamarin.iOS"
                },
                new Tag()
                {
                    Name = "Xamarin.Forms",
                    Slug = "xamarin-forms",
                    Description = "Aqui você vai encontrar ajuda relacionada a Xamarin.Forms"
                }
            };
            tags.ForEach(t => context.Tags.Add(t));

            List<Content> contents = new List<Content>()
            {
                new Content()
                {
                    Name = "Participe do Xamarin Summit 2017",
                    Description = "Nos dias 26 e 27 de Maio 2017 em São Paulo, vai rolar o maior encontro de desenvolvedores Xamarin da América Latina",
                    Tag = tags[0],
                    Banner = "http://xamarinsummit.com.br/img/new_front.jpg",
                    Url =  "http://xamarinsummit.com.br/"
                },
                new Content()
                {
                    Name = "Resolvendo o problema ‘Deployment failed because of an internal error: Failure [INSTALL_FAILED_UPDATE_INCOMPATIBLE]’",
                    Description = "Esse erro ocorreu pois eu compilei o app com uma versão do Xamarin Studio, atualizei a IDE, e tentei compilar novamente.",
                    Tag = tags[1],
                    Banner = "http://res.cloudinary.com/https-xamarinbr-azurewebsites-net/image/upload/v1435031581/code-sharing-2_khe8vn.png",
                    Url =  "http://xamarinbr.azurewebsites.net/xamarin-android-resolvendo-o-problema-deployment-failed-because-of-an-internal-error-failure-install_failed_update_incompatible/"
                },
                new Content()
                {
                    Name = "Criando um App iOS usando o Visual Studio e Xamarin",
                    Description = "Eu preciso de um Mac para criar um app para iOS? Como eu faço para usar o Xamarin dentro do Visual Studio?",
                    Tag = tags[2],
                    Banner = "http://res.cloudinary.com/https-xamarinbr-azurewebsites-net/image/upload/v1435031581/code-sharing-2_khe8vn.png",
                    Url =  "http://xamarinbr.azurewebsites.net/criando-um-app-ios-usando-o-visual-studio-e-xamarin/"
                },
                new Content()
                {
                    Name = "Tutorial: Facebook Login com Xamarin Forms",
                    Description = "Neste exemplo eu mostro como utilizar o login do Facebook com Xamarin Forms.",
                    Tag = tags[3],
                    Banner = "http://frases-para-status.com/wp-content/uploads/2016/07/status-para-facebook-810x400.png",
                    Url =  "https://www.youtube.com/watch?v=cyq_ho4QflQ&index=9&list=PLNTCwkT5owTT0elAbegf6Akf1Mf0_HAbL"
                },
            };
            contents.ForEach(c => context.Contents.Add(c));
        }
    }
}