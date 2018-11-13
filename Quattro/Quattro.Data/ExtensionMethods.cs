namespace Quattro.Data
{
	using System;
	using System.Linq;
	using System.Reflection;

	public static class ExtensionMethods
	{
		#region Listas de tipos
		public static Type[] TiposInteger = new Type[] {
			typeof(byte), typeof(sbyte), typeof(short), typeof(int), typeof(long),
			typeof(ushort), typeof(uint), typeof(ulong), typeof(bool), typeof(TimeSpan) };

		public static Type[] TiposReal = new Type[] {
			typeof(float), typeof(double), typeof(decimal) };

		public static Type[] TiposText = new Type[] {
			typeof(char), typeof(string), typeof(DateTime), typeof(DateTimeOffset), typeof(Guid) };

		public static Type[] TiposBlob = new Type[] {
			typeof(byte[]), typeof(sbyte[]), };
		#endregion


		#region Getters de valores
		public static string GetSqlValue(this object dato)
		{
			if (dato is decimal d) return d.ToString().Replace(",", ".");
			if (dato is bool b) return b ? "1" : "0";
			if (dato is TimeSpan ts) return ts.Ticks.ToString();
			if (dato is DateTime dt) return dt.ToString("yyyy-MM-dd");
			if (dato is Guid g) return g.ToString();

			return dato.ToString();
		}
		#endregion


		#region Setters de valores
		public static void SetSqlValue(this bool dato, int valor) => dato = valor == 1;
		public static void SetSqlValue(this TimeSpan dato, int valor) => dato = new TimeSpan(valor);

		public static void SetSqlValue(this DateTime dato, string valor) => DateTime.TryParse(valor, out dato);
		public static void SetSqlValue(this Guid dato, string valor) => dato = new Guid(valor);
		#endregion


		public static string GetSqlType(this PropertyInfo prop)
		{
			if (TiposInteger.Contains(prop.PropertyType)) return "INTEGER";
			if (TiposReal.Contains(prop.PropertyType)) return "REAL";
			if (TiposBlob.Contains(prop.PropertyType)) return "BLOB";
			return "TEXT";
		}



	}
}
