using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Halper;
using UnityEngine;

namespace Models.Managers
{
  public class PersonManager
  {
    private string _fileName = "persons";
    private static PersonManager _instance;
    public static PersonManager Get() => _instance ?? (_instance = new PersonManager());
    public Dictionary<int, Person> Persons { get; set; }

    private PersonManager()
    {
      Persons = new Dictionary<int, Person>();
      
      var personsSprites = Resources.LoadAll<Sprite>(_fileName);
      foreach (var personSprite in personsSprites)
      {
        BindSpriteWithPerson(personSprite);
      }
    }
    
    private Person GetPersonFrom(int index)
    {
      if (!Persons.ContainsKey(index))
      {
        var infos = default(Person[]);
        if (CultureInfo.InstalledUICulture.Name == "en-US")
          infos = JsonHelper.FromJson<Person>(Resources.Load("Metadata/PersonsInfo_en").ToString());
        else infos = JsonHelper.FromJson<Person>(Resources.Load("Metadata/PersonsInfo_ru").ToString());
        
        var info = infos.First(x => x.index == index);
        
        Persons.Add(index, new Person
        {
          index = index,
          name = info.name,
          description = info.description,
          gunIndex = info.gunIndex,
          price = info.price
        });
      }

      return Persons[index];
    }

    private void BindSpriteWithPerson(Sprite sprite)
    {
      var args = sprite.name.Split('_').Skip(1).ToList();

      if (args.Count > 1)
      {
        var index = int.Parse(args[0]);
        var person = GetPersonFrom(index);

        if (args[1] == "head")
        {
          person.SpriteHead = sprite;
        }
        else if (args[1] == "body")
        {
          person.SpriteBody = sprite;
        }
        else if (args[1] == "hand")
        {
          if (args[2] == "top")
          {
            person.SpriteHandTop = sprite;
          }
          else if (args[2] == "bottom")
          {
            person.SpriteHandBottom = sprite;
          }
        }
        else if (args[1] == "leg")
        {
          if (args[2] == "top")
          {
            person.SpriteLegTop = sprite;
          }
          else if (args[2] == "bottom")
          {
            person.SpriteLegBottom = sprite;
          }
        }
        else if (args[1] == "foot")
        {
          person.SpriteFoot = sprite;
        }
      }
    }
  }
}