using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateModel : Model 
{
    public PersonData data { get; private set; }

    public override void Init()
    {
        base.Init();
        data = PersonData.Instance.Get();
        data.atts = new int[PersonData.AttTypeCount];
        
        RandomAtt();
        
    }

    public void RandomAtt()
    {
        for (int i = 0; i < 10; i++)
        {
            int value = Random.Range(10, 51);
            data.atts[i] = value;
        }
        EventCenter.Broadcast(GameEventType.RefreshUI);
    }

    public void CreatPlayer()
    {
        WindowManager.Instance.Close<CreateWindow>();
        var person = PersonManager.Instance.CreatePerson(data);
        GameManager.Instance.SetPlayer(person);
    }
    
    

}
