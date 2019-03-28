using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtfer.Ecp.Unity
{
    public interface ISceneType
    {
        string Name { get; }
        bool Is<TScene>() where TScene : ISceneType;
    }

    public abstract class SceneTypeBase<T> : ISceneType
        where T : ISceneType
    {
        public abstract string Name { get; }
        private Type SceneType => typeof(T);

        public bool Is<TScene>()
            where TScene : ISceneType
        {
            return SceneType == typeof(TScene);
        }
    }
}
