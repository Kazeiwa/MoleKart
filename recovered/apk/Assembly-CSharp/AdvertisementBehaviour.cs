using System.Collections;
using Misc;
using UnityEngine;

public class AdvertisementBehaviour : MonoBehaviour
{
	public enum StateID
	{
		None,
		Init,
		FullOpenFirst,
		FullOpen,
		ToClose,
		FullClose,
		ToOpen,
		Clicked
	}

	public enum ButtonID
	{
		None,
		Close,
		Open
	}

	public interface IAdvertisementState
	{
		void OnGUI();
	}

	public class State : SuperFSM<StateID, AdvertisementBehaviour>.State, IAdvertisementState
	{
		public State(StateID stateID, AdvertisementBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}

		public virtual void OnGUI()
		{
		}
	}

	public class InitState : State
	{
		public InitState(StateID stateID, AdvertisementBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}

		public override void ExitAction()
		{
			base.Behaviour.LoadButtonImages();
		}
	}

	public class FullOpenState : State
	{
		public FullOpenState(StateID stateID, AdvertisementBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}

		public override void EntryAction()
		{
			base.Behaviour.ButtonClicked = false;
		}

		public override void OnGUI()
		{
			base.Behaviour.GuiShowAdvertisement(1f, ButtonID.Close);
		}
	}

	public class ToCloseState : State
	{
		public ToCloseState(StateID stateID, AdvertisementBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}

		public override void OnGUI()
		{
			base.Behaviour.GuiShowAdvertisement(1f - base.Timer.SaturatedElapsedRate, ButtonID.Close);
		}
	}

	public class FullCloseState : State
	{
		public FullCloseState(StateID stateID, AdvertisementBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}

		public override void EntryAction()
		{
			base.Behaviour.ButtonClicked = false;
		}

		public override void OnGUI()
		{
			base.Behaviour.GuiShowAdvertisement(0f, ButtonID.Open);
		}
	}

	public class ToOpenState : State
	{
		public ToOpenState(StateID stateID, AdvertisementBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}

		public override void OnGUI()
		{
			base.Behaviour.GuiShowAdvertisement(base.Timer.SaturatedElapsedRate, ButtonID.Open);
		}
	}

	public class ClickedState : State
	{
		public ClickedState(StateID stateID, AdvertisementBehaviour behaviour)
			: base(stateID, behaviour)
		{
		}

		public override void EntryAction()
		{
			base.Behaviour.SendClick();
		}
	}

	private const float ImageMovingTimeLength = 1f;

	private static string baseInfoUrl = "http://wlad.61.com/advertise.fcgi";

	private string url;

	private Texture2D image;

	private GUIStyle imageGUIStyle = new GUIStyle();

	private Texture2D imageButton01;

	private Texture2D imageButton02;

	private Vector2 resolutionScale;

	private SuperFSM<StateID, AdvertisementBehaviour> fsm;

	public bool ButtonClicked { get; set; }

	public bool AdClicked { get; set; }

	public bool CanSendClick { get; set; }

	public bool IsImageAvailable => image != null && image.width > 8 && image.height > 8 && image.width <= Screen.width && image.height <= Screen.height;

	public float ButtonWidth { get; set; }

	public string Platform { get; set; }

	public string ChannelId => "icar_" + AppMain.Instance.currVersion + "_" + AppMain.Instance.channel;

	public string UDID
	{
		get
		{
			string text = SystemInfo.deviceUniqueIdentifier;
			if (text == null)
			{
				text = "000000000000000000000000000";
			}
			return text;
		}
	}

	public string Lang => "zh-Hans";

	public AdInfo AdInfo { get; set; }

	public string UrlGetAdvertiseContent
	{
		get
		{
			string text = ((Application.internetReachability != NetworkReachability.ReachableViaLocalAreaNetwork) ? "wwan" : "wifi");
			string text2 = "android";
			string text3 = "icar";
			string text4 = Screen.width + "*" + Screen.height;
			string text5 = SystemInfo.operatingSystem.Replace(' ', '_');
			string text6 = "Unknown";
			return baseInfoUrl + $"?Type=get_advertise_content&UDID={UDID}&Channel={ChannelId}&Plat={text2}&Lang={Lang}&Net={text}&PlatVersion={text5}&GameName={text3}&GameVersion={AppMain.Instance.currVersion}&DeviceType={text6}&Resolution={text4}";
		}
	}

	public string UrlAddClickStat
	{
		get
		{
			string arg = ((AdInfo.ID == null) ? string.Empty : AdInfo.ID);
			return baseInfoUrl + $"?Type=add_click_stat&Channel={ChannelId}&UDID={UDID}&Appname={arg}";
		}
	}

	public AdvertisementBehaviour()
	{
		ButtonClicked = false;
		AdClicked = false;
		CanSendClick = false;
		AdInfo = new AdInfo();
	}

	public void SendClick()
	{
		StartCoroutine(SendClickRequest());
	}

	public void GuiShowAdvertisement(float positionRate, ButtonID buttonID)
	{
		float num = (float)Screen.width - positionRate * (float)image.width * resolutionScale.y;
		Rect position = new Rect(num, 0f, (float)image.width * resolutionScale.y, (float)image.height * resolutionScale.y);
		if (num > 0f && GUI.Button(position, image, imageGUIStyle))
		{
			Application.OpenURL(url);
			AdClicked = true;
		}
		switch (buttonID)
		{
		case ButtonID.Close:
			position.width = ButtonWidth;
			position.x -= position.width;
			position.height = (float)imageButton01.height * resolutionScale.y;
			if (GUI.Button(position, imageButton01, imageGUIStyle))
			{
				ButtonClicked = true;
			}
			break;
		case ButtonID.Open:
			position.width = ButtonWidth;
			position.x -= position.width;
			position.height = (float)imageButton02.height * resolutionScale.y;
			if (GUI.Button(position, imageButton02, imageGUIStyle))
			{
				ButtonClicked = true;
			}
			break;
		case ButtonID.None:
			break;
		default:
			Debug.LogError("Unknown button ID: " + buttonID);
			break;
		}
	}

	protected void Awake()
	{
		string currentPlatform = AppMain.Instance.GetCurrentPlatform();
		if (currentPlatform == null || currentPlatform == string.Empty)
		{
			currentPlatform = "ipad";
		}
		Platform = "ipad";
		resolutionScale = new Vector2((float)Screen.width / 1024f, Mathf.Min((float)Screen.height / 768f * 1.25f, 1f));
		ButtonWidth = 28f * resolutionScale.y;
	}

	protected IEnumerator Start()
	{
		MakeFSM();
		string infoUrl = UrlGetAdvertiseContent;
		WWW infoWww = new WWW(infoUrl);
		yield return infoWww;
		if (infoWww.error != null)
		{
			yield break;
		}
		AdInfo info = AdInfo;
		AdInfoReader reader = new AdInfoReader(info);
		if (reader.Read(infoWww.bytes))
		{
			url = info.DownloadUrl;
			WWW imageWww = new WWW(info.ImageUrl);
			yield return imageWww;
			if (imageWww.error == null)
			{
				image = imageWww.texture;
			}
		}
	}

	protected void Update()
	{
		fsm.Update(doStateActionIfNotTriggered: true);
	}

	protected void OnGUI()
	{
		IAdvertisementState advertisementState = fsm.CurrentState as IAdvertisementState;
		advertisementState.OnGUI();
	}

	protected void MakeFSM()
	{
		fsm = new SuperFSM<StateID, AdvertisementBehaviour>(this);
		fsm.CreateState<InitState>(StateID.Init).CreateTransition(StateID.FullOpenFirst, (AdvertisementBehaviour b) => b.IsImageAvailable);
		fsm.CreateState<FullOpenState>(StateID.FullOpenFirst).CreateTransition(StateID.Clicked, (AdvertisementBehaviour b) => b.AdClicked).CreateTransition(StateID.ToClose, (AdvertisementBehaviour b) => b.ButtonClicked)
			.CreateTimedTransition(5f, StateID.ToClose);
		fsm.CreateState<FullOpenState>(StateID.FullOpen).CreateTransition(StateID.Clicked, (AdvertisementBehaviour b) => b.AdClicked).CreateTransition(StateID.ToClose, (AdvertisementBehaviour b) => b.ButtonClicked);
		fsm.CreateState<ToCloseState>(StateID.ToClose).CreateTransition(StateID.Clicked, (AdvertisementBehaviour b) => b.AdClicked).CreateTimedTransition(1f, StateID.FullClose);
		fsm.CreateState<FullCloseState>(StateID.FullClose).CreateTransition(StateID.Clicked, (AdvertisementBehaviour b) => b.AdClicked).CreateTransition(StateID.ToOpen, (AdvertisementBehaviour b) => b.ButtonClicked);
		fsm.CreateState<ToOpenState>(StateID.ToOpen).CreateTransition(StateID.Clicked, (AdvertisementBehaviour b) => b.AdClicked).CreateTimedTransition(1f, StateID.FullOpen);
		fsm.CreateState<ClickedState>(StateID.Clicked);
	}

	private IEnumerator SendClickRequest()
	{
		string url = UrlAddClickStat;
		yield return new WWW(url);
	}

	private void LoadButtonImages()
	{
		imageButton01 = Resources.Load("UI/Ad/AdBtn01_" + Platform) as Texture2D;
		imageButton02 = Resources.Load("UI/Ad/AdBtn02_" + Platform) as Texture2D;
	}
}
