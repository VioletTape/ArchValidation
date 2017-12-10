using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using FluentAssertions;
using NUnit.Framework;

namespace DomainA {
    /*
     * Following example is about [Protected] attribute
     *
     * No one can use protected ctor/method
     * except internal implementation
     */

    [Serializable]
    public class PostCard {
        public string Destination { get; set; }

        public PostCard(string destination) {
            Destination = destination;
        }

        /*
         * Uncomment, to remove errors of serialization
         * The Internal keyword wouldn't help to protect ctor
         */
//        [Protected(Severity = SeverityType.Error)]
//        public PostCard() { }
    }

    public class PostOffice {
        public string Send(PostCard card) {
            var serializer = new XmlSerializer(typeof(PostCard));
            var stringWriter = new StringWriter(new StringBuilder());

            serializer.Serialize(stringWriter, card);

            stringWriter.Close();
            return stringWriter.ToString();
        }

        public PostCard Recieve(string cardData) {
            var serializer = new XmlSerializer(typeof(PostCard));
            var stream = new StringReader(cardData);
            return (PostCard) serializer.Deserialize(stream);
        }
    }

    [TestFixture]
    public class SerizalizationTest {
        [Test]
        public void CheckSendAndRecieve() {
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