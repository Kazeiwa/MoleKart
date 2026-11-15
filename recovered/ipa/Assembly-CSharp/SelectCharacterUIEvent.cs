using System.Collections.Generic;
using UnityEngine;
using iap;

public class SelectCharacterUIEvent : MonoBehaviour
{
	private enum CharacterSound
	{
		none = 0,
		momo = 30101,
		rk = 30201,
		lele = 30501,
		fred = 30301,
		mmt = 30405,
		kula = 30601,
		puti = 30701,
		yali = 30801
	}

	private const string characterName = "ShowCharacter";

	private const string carName = "ShowCar";

	private bool onEnter;

	private CharacterSound lastPlay;

	private bool changeState;

	private ushort lastID;

	private RacingType iRacingType;

	private float newScaleNum;

	private ushort oldID;

	private float currentScale;

	private bool onChange;

	private float switchSpeed;

	private float startTime;

	private float currentTime;

	private Vector3 beginPos;

	private Vector3 endPos;

	private Vector3 oriPos;

	private Dictionary<int, int> m_CarTypeToIndex;

	private GameObject m_ReleaseBg;

	private GameObject m_Purchase;

	private GameObject m_Proceeding;

	private GameObject m_ReleaseTitle;

	private string m_PurchasePrice;

	private GameObject m_GoBtn;

	private int m_BuyID;

	private int m_Index;

	private Dictionary<string, string> m_AlertBoxMap;

	private string m_TextFilePath;

	private Dictionary<int, Vector3> rolePos;

	private Dictionary<int, Quaternion> roleRotation;

	private Dictionary<int, Vector3> roleScale;

	private Dictionary<int, Vector3> carPos;

	private Dictionary<int, Quaternion> carRotation;

	private Dictionary<int, Vector3> carScale;

	private bool m_Started;

	private ushort m_CurrentID;

	private ushort m_CurrentUserID;

	public string CharecterName
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public string CarName
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	private void LoadAlertBoxText()
	{
	}

	private void CheckNewRelease()
	{
	}

	private void CheckNewRelease(int type, ref bool aButton)
	{
	}

	private void CheckEnable()
	{
	}

	private void CheckEnable(int index, int type)
	{
	}

	private void CheckSpecial()
	{
	}

	private void CheckSpecial(int index, CarModeType type)
	{
	}

	private bool Init()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void Uninit()
	{
	}

	private void FindSaveObject()
	{
	}

	private void Start()
	{
	}

	public void OnBack()
	{
	}

	public void OnRace()
	{
	}

	public void SaveSelectedCharacter(ushort id)
	{
	}

	private bool CheckPreviousSelected(RoleType role)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void CharacterSelectedMomo()
	{
	}

	public void CharacterSelectedRk()
	{
	}

	public void CharacterSelectedLele()
	{
	}

	public void CharacterSelectedFred()
	{
	}

	public void CharacterSelectedMmt()
	{
	}

	public void CharacterSelectedKula()
	{
	}

	public void CharacterSelectedPuti()
	{
	}

	public void CharacterSelectedYali()
	{
	}

	private bool CheckRelease(int id, int index)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void HidePurchasePanel()
	{
	}

	private void ShowRecordReleasePanel(string text, string proceeding)
	{
	}

	private void ShowWidthRecordReleasePanel(string text, string proceeding)
	{
	}

	private void HideRecordReleasePanel()
	{
	}

	private void ShowPurchasePanel()
	{
	}

	public void ChangeCharacter(ushort carId, bool release)
	{
	}

	public void DeletePreviousSelected()
	{
	}

	private void PlayCharacterSound(CharacterSound sound)
	{
	}

	public void ChangeCarModel(int carID, int roleID)
	{
	}

	public void PlayCharacterAnimation(SelectedRole player)
	{
	}

	public void FixedUpdate()
	{
	}

	private void ChangeState(ushort oldID, ushort newID)
	{
	}

	private void ResetAllToBeginPos()
	{
	}

	private void ResetAllToBeginPos(int except)
	{
	}

	private void ResetStatePosition(int oldID, int selectedID)
	{
	}

	private void ResetStatePosition(int oldID)
	{
	}

	private void PlayKulaFire(ref SelectedRole player)
	{
	}

	private void PlayYaliFire(ref SelectedRole player)
	{
	}

	private bool CheckCarIdIsLegal(CarModeType id)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void EnablePanel()
	{
	}

	private void DisablePanel()
	{
	}

	private void ReleaseTrack()
	{
	}

	private void PurchaseOver()
	{
	}

	public void Buy()
	{
	}

	public void PurchaseCallback(Request.Status current_status)
	{
	}

	public void ShareWeibo()
	{
	}

	public void ShareFacebook()
	{
	}

	private static string BuyIdToText(int buyId)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
