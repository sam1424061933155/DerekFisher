using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace GameSlyce
{
    public class LoadingManager : MonoBehaviour
    {
        public void LoadLevel(int id)
        {
            Application.LoadLevel("Listview"+ id);
        }
    }
}