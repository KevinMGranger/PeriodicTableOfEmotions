using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Conversation
{
    [RequireComponent(typeof(Image))]
    public class Picture : MonoBehaviour
    {
        Image img;

        void Awake()
        {
            this.CheckComponent(ref img);
        }

        void Start() { }

        void Update() { }

        public void SetImage(Sprite mat)
        {
            img.sprite = mat;
        }
    }
}