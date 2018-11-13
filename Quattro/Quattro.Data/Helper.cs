namespace Quattro.Data
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Reflection;
	using System.Text;

	public class Helper
	{

		// ====================================================================================================
		#region COMANDOS CREAR
		// ====================================================================================================

		public static ComandoSql<T> ComandoCrearTabla<T>() where T : IDataModel
		{
			var resultado = new StringBuilder();
			var foraneas = new StringBuilder();
			var tipo = typeof(T);
			// Nombre de la tabla.
			var tableName = (tipo.GetCustomAttribute(typeof(Tabla)) as Tabla)?.Nombre ?? $"{tipo.Name}";
			resultado.Append($"CREATE TABLE {tableName} ( ");
			// Propiedades
			var props = tipo.GetProperties();
			string definicion = string.Empty;
			string foraneaTemporal = string.Empty;
			foreach (var prop in props)
			{
				var atts = prop.GetCustomAttributes().ToList();
				// Ignorar
				if (atts.Any(a => a is Ignorar)) continue;
				// Ponemos la coma si no es la primera definición.
				definicion = string.IsNullOrEmpty(definicion) ? "\n" : ",\n";
				// Nombre de campo
				definicion += $"    {(prop.GetCustomAttribute(typeof(Columna)) as Columna)?.Nombre ?? prop.Name} ";
				// Tipo del campo
				definicion += $"{prop.GetSqlType()} ";
				// Not Null
				if (prop.GetCustomAttribute(typeof(NotNull)) != null)
					definicion += "NOT NULL ";
				// Primary Key
				if (prop.GetCustomAttribute(typeof(Clave)) != null)
					definicion += $"CONSTRAINT PK_{tableName} PRIMARY KEY AUTOINCREMENT ";
				// Clave Foránea
				if (prop.GetCustomAttribute(typeof(Foranea)) != null)
				{
					// Ponemos la coma si no es la primera definición.
					foraneaTemporal = string.IsNullOrEmpty(foraneaTemporal) ? "\n" : ",\n";
					var att = prop.GetCustomAttribute(typeof(Foranea)) as Foranea;
					Type tipoForanea = prop.PropertyType.GetGenericArguments()[0];
					string nombreTablaForanea = (tipoForanea.GetCustomAttribute(typeof(Tabla)) as Tabla)?.Nombre ?? $"{tipoForanea.Name}";
					string idForanea = string.Empty;
					foreach (var p in tipoForanea.GetProperties())
					{
						if (p.GetCustomAttribute(typeof(Clave)) != null)
						{
							idForanea = (p.GetCustomAttribute(typeof(Columna)) as Columna)?.Nombre ?? p.Name;
							break;
						}
					}
					foraneaTemporal += $"    CONSTRAINT FK_{tableName}_{nombreTablaForanea}_{att.ClaveForanea} \n" +
						$"      FOREIGN KEY ({att.ClaveForanea}) REFERENCES {nombreTablaForanea} ({idForanea}) {OnDelete(att.OnDelete)} {OnUpdate(att.OnUpdate)} ";

					foraneas.Append(foraneaTemporal);
				}
				// Añadimos el campo.
				resultado.Append(definicion);
			}
			if (!string.IsNullOrEmpty(foraneas.ToString())) resultado.Append($", {foraneas}");
			resultado.Append("\n)");

			return new ComandoSql<T>(resultado.ToString());
		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region COMANDOS GET
		// ====================================================================================================


		public static ComandoSql<T> ComandoGet<T>() where T : IDataModel
		{
			var tipo = typeof(T);
			var tableName = (tipo.GetCustomAttribute(typeof(Tabla)) as Tabla)?.Nombre ?? $"{tipo.Name}";
			return new ComandoSql<T>($"SELECT * FROM {tableName} ");
		}


		//public static string ComandoGetOrderBy<T>(Expression<Func<T, object>> orderBy, bool desc = false) where T : IDataModel
		//{
		//	var tipo = typeof(T);
		//	var tableName = (tipo.GetCustomAttribute(typeof(Tabla)) as Tabla)?.Nombre ?? $"{tipo.Name}";
		//	var member = orderBy.Body as MemberExpression ?? (orderBy.Body as UnaryExpression)?.Operand as MemberExpression;
		//	var prop = member?.Member as PropertyInfo;
		//	var nombreProp = (prop?.GetCustomAttribute(typeof(Columna)) as Columna)?.Nombre ?? prop?.Name ?? "";
		//	return $"SELECT * FROM {tableName} ORDER BY {nombreProp} {(desc ? "DESC" : string.Empty)};";
		//}


		//public static string ComandoGetOrderBy<T>(Expression<Func<T, object>> orderBy1, Expression<Func<T, object>> orderBy2, bool desc1 = false, bool desc2 = false) where T : IDataModel
		//{
		//	var tipo = typeof(T);
		//	var tableName = (tipo.GetCustomAttribute(typeof(Tabla)) as Tabla)?.Nombre ?? $"{tipo.Name}";
		//	var member1 = orderBy1.Body as MemberExpression ?? (orderBy1.Body as UnaryExpression)?.Operand as MemberExpression;
		//	var member2 = orderBy2.Body as MemberExpression ?? (orderBy2.Body as UnaryExpression)?.Operand as MemberExpression;
		//	var prop1 = member1?.Member as PropertyInfo;
		//	var nombreProp1 = (prop1?.GetCustomAttribute(typeof(Columna)) as Columna)?.Nombre ?? prop1?.Name ?? "";
		//	var prop2 = member2?.Member as PropertyInfo;
		//	var nombreProp2 = (prop2?.GetCustomAttribute(typeof(Columna)) as Columna)?.Nombre ?? prop2?.Name ?? "";
		//	return $"SELECT * FROM {tableName} ORDER BY {nombreProp1} {(desc1 ? "DESC" : string.Empty)}, {nombreProp2} {(desc2 ? "DESC" : string.Empty)};";

		//}


		//public static string ComandoGetWhere<T>(Expression<Func<T, bool>> pred) where T : IDataModel
		//{
		//	var tipo = typeof(T);
		//	var tableName = (tipo.GetCustomAttribute(typeof(Tabla)) as Tabla)?.Nombre ?? $"{tipo.Name}";
		//	var where = ConvertirWhere(pred);
		//	return $"SELECT * FROM {tableName} WHERE {where};";
		//}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region COMANDOS INSERT
		// ====================================================================================================


		public static string ComandoInsert<T>(T model) where T : IDataModel
		{
			// Variables iniciales.
			var columnas = new StringBuilder();
			var valores = new StringBuilder();
			var tipo = typeof(T);
			// Nombre de la tabla.
			var tableName = (tipo.GetCustomAttribute(typeof(Tabla)) as Tabla)?.Nombre ?? $"{tipo.Name}";
			// Propiedades
			var props = tipo.GetProperties();
			string separador = string.Empty;
			foreach (var prop in props)
			{
				var atts = prop.GetCustomAttributes().ToList();
				// Ignorar y clave
				if (atts.Any(a => a is Ignorar)) continue;
				if (atts.Any(a => a is Clave)) continue;
				if (atts.Any(a => a is Foranea)) continue;
				// Ponemos la coma si no es la primera definición.
				separador = string.IsNullOrEmpty(separador) ? "\n" : ",\n";
				// Nombre de campo
				columnas.Append($"{separador}    {(prop.GetCustomAttribute(typeof(Columna)) as Columna)?.Nombre ?? prop.Name} ");
				if (prop.GetSqlType() == "TEXT")
				{
					valores.Append($"{separador}    '{tipo.GetProperty(prop.Name).GetValue(model).GetSqlValue()}' ");
				} else {
					valores.Append($"{separador}    {tipo.GetProperty(prop.Name).GetValue(model).GetSqlValue()} ");
				}
			}

			return $"INSERT INTO {tableName} ({columnas.ToString()} \n) VALUES ({valores.ToString()});";

		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region COMANDOS UPDATE
		// ====================================================================================================

		public static string ComandoUpdate<T>(T model) where T : IDataModel
		{
			// Variables iniciales.
			var columnas = new StringBuilder();
			var valores = new StringBuilder();
			var tipo = typeof(T);
			// Nombre de la tabla.
			var tableName = (tipo.GetCustomAttribute(typeof(Tabla)) as Tabla)?.Nombre ?? $"{tipo.Name}";
			// Propiedades
			var props = tipo.GetProperties();
			string separador = string.Empty;
			string campoClave = string.Empty;
			string valorClave = string.Empty;
			foreach (var prop in props)
			{
				var atts = prop.GetCustomAttributes().ToList();
				// Ignorar y clave
				if (atts.Any(a => a is Ignorar)) continue;
				if (atts.Any(a => a is Foranea)) continue;
				if (prop.GetCustomAttribute(typeof(Clave)) != null)
				{
					campoClave = (prop.GetCustomAttribute(typeof(Columna)) as Columna)?.Nombre ?? prop.Name;
					if (prop.GetSqlType() == "TEXT")
					{
						valorClave = $"'{tipo.GetProperty(prop.Name).GetValue(model).GetSqlValue()}' ";
					} 
					else
					{
						valorClave = $"{tipo.GetProperty(prop.Name).GetValue(model).GetSqlValue()} ";
					}
					continue;
				}
				// Ponemos la coma si no es la primera definición.
				separador = string.IsNullOrEmpty(separador) ? "\n" : ",\n";
				// Nombre de campo
				columnas.Append($"{separador}    {(prop.GetCustomAttribute(typeof(Columna)) as Columna)?.Nombre ?? prop.Name} = ");
				if (prop.GetSqlType() == "TEXT")
				{
					columnas.Append($"'{tipo.GetProperty(prop.Name).GetValue(model).GetSqlValue()}' ");
				} 
				else
				{
					columnas.Append($"{tipo.GetProperty(prop.Name).GetValue(model).GetSqlValue()} ");
				}
			}

			return $"UPDATE {tableName} SET {columnas.ToString()} \n WHERE {campoClave} = {valorClave};";

		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS PÚBLICOS
		// ====================================================================================================

		public static string Field<T>(Expression<Func<T, object>> pred)
		{
			var member = pred.Body as MemberExpression ?? (pred.Body as UnaryExpression)?.Operand as MemberExpression;
			var prop = member?.Member as PropertyInfo;
			return (prop?.GetCustomAttribute(typeof(Columna)) as Columna)?.Nombre ?? prop?.Name ?? "";
		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS PRIVADOS ESTÁTICOS
		// ====================================================================================================

		private static string OnDelete(ForeignKeyActions accion)
		{
			switch (accion)
			{
				case ForeignKeyActions.Restrict: return "ON DELETE RESTRICT";
				case ForeignKeyActions.SetNull: return "ON DELETE SET NULL";
				case ForeignKeyActions.SetDefault: return "ON DELETE SET DEFAULT";
				case ForeignKeyActions.Cascade: return "ON DELETE CASCADE";
			}
			return string.Empty;
		}


		private static string OnUpdate(ForeignKeyActions accion)
		{
			switch (accion)
			{
				case ForeignKeyActions.Restrict: return "ON UPDATE RESTRICT";
				case ForeignKeyActions.SetNull: return "ON UPDATE SET NULL";
				case ForeignKeyActions.SetDefault: return "ON UPDATE SET DEFAULT";
				case ForeignKeyActions.Cascade: return "ON UPDATE CASCADE";
			}
			return string.Empty;
		}


		private static string ConvertirWhere<T>(Expression<Func<T, bool>> pred) where T : IDataModel
		{
			var member = pred.Body as BinaryExpression;
			var tipo = typeof(T);
			var campos = pred.ToString().Split(new string[] { "=>" }, StringSplitOptions.None);
			var lambda = $"{campos[0].Trim()}.";
			var resultado = campos[1].Trim().Replace(".Contains", " LIKE ");
			var props = resultado.Split(' ');

			foreach (var p in props)
			{
				var s = p.Replace("(", "").Replace(")", "");
				if (s.StartsWith(lambda))
				{
					var propiedad = tipo.GetProperty(s.Substring(lambda.Length));
					var nombreProp = (propiedad?.GetCustomAttribute(typeof(Columna)) as Columna)?.Nombre ?? propiedad?.Name ?? "";
					resultado = resultado.Replace($"{lambda}{propiedad?.Name}", $"{nombreProp}");
				}
			}
			resultado = resultado.Replace("AndAlso", "AND").Replace("OrElse", "OR").Replace("==", "=").Replace("!=", "<>").Replace("\"", "'");

			return resultado;
		}

		#endregion
		// ====================================================================================================




		private static string ConvertirWhere2<T>(Expression<Func<T, bool>> pred) where T : IDataModel
		{
			var member = pred.Body as BinaryExpression;
			var tipo = typeof(T);
			var campos = pred.ToString().Split(new string[] { "=>" }, StringSplitOptions.None);
			var lambda = $"{campos[0].Trim()}.";
			var resultado = campos[1].Trim().Replace(".Contains", " LIKE ");
			var props = resultado.Split(' ');

			foreach (var p in props)
			{
				var s = p.Replace("(", "").Replace(")", "");
				if (s.StartsWith(lambda))
				{
					var propiedad = tipo.GetProperty(s.Substring(lambda.Length));
					var nombreProp = (propiedad?.GetCustomAttribute(typeof(Columna)) as Columna)?.Nombre ?? propiedad?.Name ?? "";
					resultado = resultado.Replace($"{lambda}{propiedad?.Name}", $"{nombreProp}");
				}
			}
			resultado = resultado.Replace("AndAlso", "AND").Replace("OrElse", "OR").Replace("==", "=").Replace("!=", "<>").Replace("\"", "'");

			return resultado;
		}





	}
}
