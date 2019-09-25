using System.Collections.Generic;
using System.IO;
using System.Linq;
using Models.Managers;

public class ProfileManager
{
  private string _fileName = "profile";
  private static ProfileManager _instance;
  public static ProfileManager Get() => _instance ?? (_instance = new ProfileManager());
  public Profile Profile { get; set; }

  private ProfileManager()
  {
    if (!File.Exists(XmlHelper.GetPath(_fileName)))
    {
      XmlHelper.Save(new Profile
      {
        AmountCoint = 0,
        CurrentPersonGun = 1,
        CurrentPersonIndex = 1,
        PersonAvialables = new List<int> { 1 }
      }, _fileName);
    }

    Profile = XmlHelper.Load<Profile>(_fileName);
  }

  public List<Person> GetRivals() =>
    PersonManager.Get().Persons
      .Where(p => p.Value.index != Profile.CurrentPersonIndex)
      .Select(p => p.Value)
      .ToList();

  public void Commit()
  {
    XmlHelper.Save(Profile, _fileName);
  }
}
