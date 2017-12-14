using System.IO;
using System.Text;
using System.Xml.Serialization;
using DomainA;
using FluentAssertions;
using NUnit.Framework;

namespace ServiceLayer {
    public class PostOffice
    {
        public string Send(PostCard card)
        {
            var serializer = new XmlSerializer(typeof(PostCard));
            var stringWriter = new StringWriter(new StringBuilder());

            serializer.Serialize(stringWriter, card);

            stringWriter.Close();
            return stringWriter.ToString();
        }

        public PostCard Recieve(string cardData)
        {
            var serializer = new XmlSerializer(typeof(PostCard));
            var stream = new StringReader(cardData);
            return (PostCard)serializer.Deserialize(stream);
        }
    }

    [TestFixture]
    public class SerizalizationTest
    {
        [Test]
        public void CheckSendAndRecieve()
        {
            // arrange 
            var postOffice = new PostOffice();
            var postCard = new PostCard("Hello CodeEurope!");


            // act
            var message = postOffice.Send(postCard);
            var card = postOffice.Recieve(message);

            // assert
            postCard.Destination.Should()
                    .Be(card.Destination);
        }
    }
}