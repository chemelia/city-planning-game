using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class Character : MonoBehaviour
{
    public struct Tweet
    {
        public string header;
        public string body;
    }

    private string mFileName;
    private XmlDocument xmlDoc;

    private List<Tweet> randomTweets;
    private List<Tweet> goodTweets;
    private List<Tweet> badTweets;
    private string profilePath;
    private string characterName;

    public Character( string fileName )
    {
        mFileName = fileName;

        randomTweets = new List<Tweet>();
        goodTweets = new List<Tweet>();
        badTweets = new List<Tweet>();
    
        loadXML();
        parseXML();
    }

    private void loadXML()
    {
        xmlDoc = new XmlDocument();
        string filePath = Util.GetPath() + mFileName + ".xml";
        Debug.Log( filePath );

        if ( System.IO.File.Exists( filePath ) )
        {
            xmlDoc.Load( filePath );
        }
    }

    private void parseXML()
    {
        XmlNode character = xmlDoc.SelectSingleNode( "character" );
        characterName = character.SelectSingleNode( "name" ).InnerText;
        profilePath = character.SelectSingleNode( "sprite" ).InnerText;

        // Random tweets
        foreach( XmlElement node in xmlDoc.SelectNodes( "character/tweets/random/tweet" ) )
        {
            Tweet tempTweet = new Tweet();
            tempTweet.header = characterName;
            tempTweet.body = node.InnerText.Trim();

            randomTweets.Add( tempTweet );    
        }
    }

    public Tweet GetRandomTweet()
    {
        int randInt = Random.Range( 0, randomTweets.Count - 1 );
		return randomTweets[randInt];
    }
}