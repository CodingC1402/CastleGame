using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils {
    public class EventHandler<TSender, TParam>
    {
        public delegate void Callback(TSender sender, TParam param);
        private List<Callback> _callbacks = new List<Callback>();

        public void Add(Callback clb) {
            _callbacks.Add(clb);
        }
        public void Remove(Callback clb) {
            _callbacks.Remove(clb);
        }
        public void Clear() {
            _callbacks.Clear();
        }

        public void Invoke(TSender sender, TParam param) {
            foreach(var clb in _callbacks) {
                clb(sender, param);
            }
        }
    }
}
