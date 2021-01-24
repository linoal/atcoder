using System.Security.AccessControl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtcoderLib{

    // 順列の全列挙。使い方はnewして foreach(var p in perm.Enumerate()) する。
    public class Permutation{
        // 参考 https://qiita.com/gushwell/items/8780fc2b71f2182f36ac
        public IEnumerable<T[]> Enumerate<T>(IEnumerable<T> items){
            if(items.Count() == 1){
                yield return new[] {items.First()};
                yield break;
            }
            foreach(var item in items){
                var leftside = new T[] {item};
                var unused = items.Except(leftside);
                foreach(var rightside in Enumerate(unused)){
                    yield return leftside.Concat(rightside).ToArray();
                }
            }
        }
    }
}