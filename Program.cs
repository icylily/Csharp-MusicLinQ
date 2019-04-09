using System;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Collections to work with
            List<Artist> Artists = MusicStore.GetData().AllArtists;
            List<Group> Groups = MusicStore.GetData().AllGroups;

            //========================================================
            //Solve all of the prompts below using various LINQ queries
            //========================================================

            //There is only one artist in this collection from Mount Vernon, what is their name and age?
            Console.WriteLine(" ");
            Console.WriteLine("This artist is from Mount Vernon:");
            IEnumerable<Artist> fromMountVernon = Artists.Where(who => who.Hometown == "Mount Vernon");
            foreach (var a in fromMountVernon)
            {
                System.Console.WriteLine("Name: " + a.ArtistName + " Age: " + a.Age);
            }
            //Who is the youngest artist in our collection of artists?
            Console.WriteLine(" ");
            Console.WriteLine("The youngerst artist is:");
            IEnumerable<Artist> youngerst = Artists.Where(artist => artist.Age == Artists.Min(a => a.Age));
            foreach (var a in youngerst)
            {
                System.Console.WriteLine("Name: " + a.ArtistName + " Age: " + a.Age);
            }
            //Display all artists with 'William' somewhere in their real name
            Console.WriteLine(" ");
            Console.WriteLine("Real name contains William");
            IEnumerable<Artist> william = Artists.Where(who=>who.RealName.Contains("William"));
            foreach (var a in william)
            {
                System.Console.WriteLine("Name: " + a.ArtistName + " Age: " + a.Age +" RealName: "+a.RealName);
            }Console.WriteLine(" ");
            //Display the 3 oldest artist from Atlanta
            Console.WriteLine("Orderst 3 from Atlanta");
            IEnumerable<Artist> orderstAtlanta = Artists.Where(who=>who.Hometown =="Atlanta").OrderByDescending(who=>who.Age).Take(3);
            foreach (var a in orderstAtlanta)
            {
                System.Console.WriteLine("Name: " + a.ArtistName + " Age: " + a.Age + " RealName: " + a.RealName+" HomeTown: " +a.Hometown);
            }
            //(Optional) Display the Group Name of all groups that have members that are not from New York City
            Console.WriteLine(" ");
            System.Console.WriteLine("all groups that have members that are not from New York City");
            IEnumerable<string> notApples = Artists.Where(artist => artist.Hometown != "New York City")
                .Join(Groups,
                    artist => artist.GroupId,
                    group => group.Id,
                    (artist, group) =>
                    {
                        return group.GroupName;
                    }).Distinct().ToArray();
            foreach (var member in notApples)
            {
                System.Console.WriteLine(member);
            }
            //(Optional) Display the artist names of all members of the group 'Wu-Tang Clan'
            Console.WriteLine(" ");
            Console.WriteLine("All members of the group 'Wu-Tang Clan'");
            IEnumerable<string> fromWuTangClan = Groups
                .Where(group=>group.GroupName == "Wu-Tang Clan")
                .Join(Artists,
                     group => group.Id,
                     who=>who.GroupId,
                     (group,who)=>
                        {
                            return who.ArtistName + " " + who.RealName + " " + group.GroupName;
                        }
                );
            foreach(string member in fromWuTangClan)
            {
                System.Console.WriteLine(member);
            }

            Console.WriteLine(Groups.Count);
        }
    }
}
