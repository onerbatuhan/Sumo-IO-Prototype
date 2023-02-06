using System.Collections.Generic;
using DesignPattern;
using UnityEngine;

namespace GameManager
{
    public class GeneralPlayerList : Singleton<GeneralPlayerList>
    {
        public List<GameObject> players;
    }
}
