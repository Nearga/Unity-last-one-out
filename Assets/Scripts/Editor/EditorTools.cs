using UnityEditor;
using UnityEditor.SceneManagement;

public class EditorTools {

	[MenuItem("Svitla/Play Menu")]
	static void BootstrapPlay()
	{
		EditorSceneManager.SaveOpenScenes();
		EditorSceneManager.OpenScene("Assets/Scenes/MainMenu.unity");
		EditorApplication.isPlaying = true;
	}

	[MenuItem("Svitla/Play Game")]
	static void DebigPlay()
	{
		EditorSceneManager.SaveOpenScenes();
		EditorSceneManager.OpenScene("Assets/Scenes/Scene_1.unity");
		EditorApplication.isPlaying = true;
	}
}
