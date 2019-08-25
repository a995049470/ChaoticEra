using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWindow : Window 
{  
    private CreateModel m_CreateModel { get { return ModelManager.Instance.Get<CreateModel>(); } }
	
}
