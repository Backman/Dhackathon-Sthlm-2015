public static class InputManager
{
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
		public static string PlayerOneVert = "P1_Vertical_WIN";
		public static string PlayerOneHoriz = "P1_Horizontal_WIN";
		
		public static string PlayerTwoVert = "P2_Vertical_WIN";
		public static string PlayerTwoHoriz = "P2_Horizontal_WIN";
		
		public static string PlayerOneXRot = "P1_XRot_WIN";
		public static string PlayerOneYRot = "P1_YRot_WIN";
		
		public static string PlayerTwoXRot = "P2_XRot_WIN";
		public static string PlayerTwoYRot = "P2_YRot_WIN";
#else
		public static string PlayerOneVert = "P1_Vertical";
		public static string PlayerOneHoriz = "P1_Horizontal";
		
		public static string PlayerTwoVert = "P2_Vertical";
		public static string PlayerTwoHoriz = "P2_Horizontal";
		
		public static string PlayerOneXRot = "P1_XRot";
		public static string PlayerOneYRot = "P1_YRot";
		
		public static string PlayerTwoXRot = "P2_XRot";
		public static string PlayerTwoYRot = "P2_YRot";
#endif
}