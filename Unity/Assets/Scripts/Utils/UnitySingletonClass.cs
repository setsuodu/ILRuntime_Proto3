using UnityEngine;

namespace Client
{
	public class UnitySingletonClass<T> : MonoBehaviour where T : UnitySingletonClass<T>
	{
		private static T m_Instance = null;
		public static T Instance
		{
			get
			{
				if (m_Instance == null)
				{
					m_Instance = FindObjectOfType(typeof(T)) as T; //在场景中查找是否已有该类型的对象
					if (m_Instance == null)
					{
						m_Instance = new GameObject("Singleton of " + typeof(T).ToString(), typeof(T)).GetComponent<T>(); //创建一个空物体，挂载该对象
						m_Instance.Init(); //如果子类中需要初始化，则在子类中重写
					}
				}
				return m_Instance;
			}
		}

		void Awake()
		{
			//if (m_Instance == null)
			//	m_Instance = this as T;
		}

		void OnApplicationQuit()
		{
			// 程序结束时，释放内存
			m_Instance = null;
		}

		public virtual void Init() { }
	}
}
