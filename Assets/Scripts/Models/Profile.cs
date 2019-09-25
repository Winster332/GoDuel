using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Models.Managers;
using UnityEngine;

public class Profile
{
  public List<int> PersonAvialables;
  public int CurrentPersonIndex;
  public int CurrentPersonGun;
  public int AmountCoint;

  public Person GetPerson()
  {
    return PersonManager.Get().Persons.First(x => x.Value.index == CurrentPersonIndex).Value;
  }

  public Gun GetGun()
  {
    return GunManager.Get().Guns.First(x => x.index == CurrentPersonIndex);
  }
}
