using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class CarConfigData : TSingleton<CarConfigData>
{
	public static Dictionary<int, CarInf> m_CarMap = new Dictionary<int, CarInf>();

	private static string xml_path = "Config/xmlConfig/CarConfig";

	private bool m_bInit;

	public bool bInited => m_bInit;

	public bool Init()
	{
		if (m_CarMap == null || m_CarMap.Count <= 0)
		{
			XmlDocument xmlDocument = new XmlDocument();
			Object obj = Resources.Load(xml_path);
			if (null != obj)
			{
				TextAsset textAsset = (TextAsset)obj;
				xmlDocument.LoadXml(textAsset.text);
				m_bInit = true;
				return ParseXml(xmlDocument);
			}
			return false;
		}
		return true;
	}

	private bool ParseXml(XmlDocument xml)
	{
		if (xml == null)
		{
			return false;
		}
		XmlNode xmlNode = xml.SelectSingleNode("Cars");
		if (xmlNode != null)
		{
			XmlNodeList xmlNodeList = xmlNode.SelectNodes("Car");
			if (xmlNodeList != null)
			{
				foreach (XmlNode item in xmlNodeList)
				{
					if (item != null)
					{
						XmlNode xmlNode3 = item.SelectSingleNode("Low_Mode");
						XmlNode xmlNode4 = item.SelectSingleNode("High_Mode");
						XmlNode xmlNode5 = item.SelectSingleNode("ID");
						XmlNode xmlNode6 = item.SelectSingleNode("type");
						XmlNode node = item.SelectSingleNode("Physics");
						if (xmlNode3 == null || xmlNode5 == null || xmlNode6 == null || xmlNode4 == null)
						{
							Debug.LogError("SceneConfig is error, please correct");
							return false;
						}
						VehicleData kPhysData = ParsePhysicsData(node);
						if (!m_CarMap.ContainsKey(int.Parse(xmlNode5.InnerText)))
						{
							CarInf value = new CarInf
							{
								low_mode = xmlNode3.InnerText,
								type = int.Parse(xmlNode6.InnerText),
								high_mode = xmlNode4.InnerText,
								kPhysData = kPhysData,
								ID = int.Parse(xmlNode5.InnerText)
							};
							m_CarMap.Add(int.Parse(xmlNode5.InnerText), value);
						}
					}
				}
				return true;
			}
			Debug.LogWarning("Have no Car node in CarConfig.xml file");
			return false;
		}
		Debug.LogWarning("Roles node is null in CarConfig.xml file");
		return false;
	}

	private VehicleData ParsePhysicsData(XmlNode node)
	{
		VehicleData vehicleData = new VehicleData();
		XmlNode xmlNode = node.SelectSingleNode("VehicleType");
		XmlNode xmlNode2 = node.SelectSingleNode("WheelBase");
		XmlNode xmlNode3 = node.SelectSingleNode("b");
		XmlNode xmlNode4 = node.SelectSingleNode("c");
		XmlNode xmlNode5 = node.SelectSingleNode("Initia");
		XmlNode xmlNode6 = node.SelectSingleNode("VehicleHeight");
		XmlNode xmlNode7 = node.SelectSingleNode("ChassisHeight");
		XmlNode xmlNode8 = node.SelectSingleNode("Mass");
		XmlNode xmlNode9 = node.SelectSingleNode("MaxSpeed");
		XmlNode xmlNode10 = node.SelectSingleNode("BigTorqueScale");
		XmlNode xmlNode11 = node.SelectSingleNode("SmallTorqueScale");
		XmlNode xmlNode12 = node.SelectSingleNode("ShiftMinSteer");
		XmlNode xmlNode13 = node.SelectSingleNode("ShiftMaxSteer");
		XmlNode xmlNode14 = node.SelectSingleNode("ShiftP1Speed");
		XmlNode xmlNode15 = node.SelectSingleNode("ShiftP1DownAcc");
		XmlNode xmlNode16 = node.SelectSingleNode("ShiftP1UpAcc");
		XmlNode xmlNode17 = node.SelectSingleNode("ShiftAng0");
		XmlNode xmlNode18 = node.SelectSingleNode("ShiftAng1");
		XmlNode xmlNode19 = node.SelectSingleNode("ShiftAng2");
		XmlNode xmlNode20 = node.SelectSingleNode("ShiftAcc0");
		XmlNode xmlNode21 = node.SelectSingleNode("ShiftAcc1");
		XmlNode xmlNode22 = node.SelectSingleNode("ShiftAcc2");
		XmlNode xmlNode23 = node.SelectSingleNode("ShiftBoostSpeed0");
		XmlNode xmlNode24 = node.SelectSingleNode("ShiftBoostSpeed1");
		XmlNode xmlNode25 = node.SelectSingleNode("ShiftBoostTime0");
		XmlNode xmlNode26 = node.SelectSingleNode("ShiftBoostTime1");
		XmlNode xmlNode27 = node.SelectSingleNode("ShiftBoostSmallTime");
		XmlNode xmlNode28 = node.SelectSingleNode("ShiftBoostBigTime");
		XmlNode xmlNode29 = node.SelectSingleNode("ShiftStiffnessFactor");
		XmlNode xmlNode30 = node.SelectSingleNode("ShiftMaxSpeed");
		XmlNode xmlNode31 = node.SelectSingleNode("ShiftAutoMotorScale");
		XmlNode xmlNode32 = node.SelectSingleNode("ShiftAutoMaxSpeed");
		XmlNode xmlNode33 = node.SelectSingleNode("ShiftAutoMaxSteer");
		vehicleData.m_iVehicleType = int.Parse(xmlNode.InnerText);
		vehicleData.fWheelBase = float.Parse(xmlNode2.InnerText);
		vehicleData.b = float.Parse(xmlNode3.InnerText);
		vehicleData.c = float.Parse(xmlNode4.InnerText);
		vehicleData.inertia = float.Parse(xmlNode5.InnerText);
		vehicleData.fVehicleHeight = float.Parse(xmlNode6.InnerText);
		vehicleData.fChassisHeight = float.Parse(xmlNode7.InnerText);
		vehicleData.m_fMass = float.Parse(xmlNode8.InnerText);
		vehicleData.fMaxSpeed = float.Parse(xmlNode9.InnerText);
		vehicleData.fSmallTorqueScale = float.Parse(xmlNode11.InnerText);
		vehicleData.fBigTorqueScale = float.Parse(xmlNode10.InnerText);
		vehicleData.fShiftMinSteer = float.Parse(xmlNode12.InnerText);
		vehicleData.fShiftMaxSteer = float.Parse(xmlNode13.InnerText);
		vehicleData.fShiftP1Speed = float.Parse(xmlNode14.InnerText);
		vehicleData.fShiftP1DownAcc = float.Parse(xmlNode15.InnerText);
		vehicleData.fShiftP1UpAcc = float.Parse(xmlNode16.InnerText);
		vehicleData.fShiftAng0 = float.Parse(xmlNode17.InnerText);
		vehicleData.fShiftAng1 = float.Parse(xmlNode18.InnerText);
		vehicleData.fShiftAng2 = float.Parse(xmlNode19.InnerText);
		vehicleData.fShiftAcc0 = float.Parse(xmlNode20.InnerText);
		vehicleData.fShiftAcc1 = float.Parse(xmlNode21.InnerText);
		vehicleData.fShiftAcc2 = float.Parse(xmlNode22.InnerText);
		vehicleData.fShiftBoostSpeed0 = float.Parse(xmlNode23.InnerText);
		vehicleData.fShiftBoostSpeed1 = float.Parse(xmlNode24.InnerText);
		vehicleData.fShiftBoostTime0 = float.Parse(xmlNode25.InnerText);
		vehicleData.fShiftBoostTime1 = float.Parse(xmlNode26.InnerText);
		vehicleData.fShiftBoostSmallTime = float.Parse(xmlNode27.InnerText);
		vehicleData.fShiftBoostBigTime = float.Parse(xmlNode28.InnerText);
		vehicleData.fShiftStiffnessFactor = float.Parse(xmlNode29.InnerText);
		vehicleData.fShiftMaxSpeed = float.Parse(xmlNode30.InnerText);
		vehicleData.fShiftAutoMotorScale = float.Parse(xmlNode31.InnerText);
		vehicleData.fShiftAutoMaxSpeed = float.Parse(xmlNode32.InnerText);
		vehicleData.fShiftAutoMaxSteer = float.Parse(xmlNode33.InnerText);
		XmlNode xmlNode34 = node.SelectSingleNode("Steering");
		if (xmlNode34 != null)
		{
			foreach (XmlNode item3 in xmlNode34)
			{
				char[] separator = new char[1] { ',' };
				string[] array = item3.InnerText.Split(separator);
				if (array.Length == 2)
				{
					VehicleData.Streering streering = new VehicleData.Streering(float.Parse(array[0]), float.Parse(array[1]));
					vehicleData.m_kSteeringCurve.Add(streering);
				}
			}
		}
		xmlNode34 = node.SelectSingleNode("Motor");
		XmlNode xmlNode36 = xmlNode34.SelectSingleNode("Forward");
		XmlNode xmlNode37 = xmlNode34.SelectSingleNode("Backward");
		if (xmlNode34 != null && xmlNode36 != null && xmlNode37 != null)
		{
			foreach (XmlNode item4 in xmlNode36)
			{
				char[] separator2 = new char[1] { ',' };
				string[] array2 = item4.InnerText.Split(separator2);
				if (array2.Length == 2)
				{
					CurvePair item = new CurvePair(float.Parse(array2[0]), float.Parse(array2[1]));
					vehicleData.m_lstMotorCurve0.Add(item);
				}
			}
			foreach (XmlNode item5 in xmlNode37)
			{
				char[] separator3 = new char[1] { ',' };
				string[] array3 = item5.InnerText.Split(separator3);
				if (array3.Length == 2)
				{
					CurvePair item2 = new CurvePair(float.Parse(array3[0]), float.Parse(array3[1]));
					vehicleData.m_lstMotorCurve1.Add(item2);
				}
			}
		}
		return vehicleData;
	}
}
