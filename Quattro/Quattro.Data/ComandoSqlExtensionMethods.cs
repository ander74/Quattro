namespace Quattro.Data
{
	using System;
	using System.Linq.Expressions;
	using System.Reflection;

	public static class ComandoSqlExtensionMethods
	{

		public static ComandoSql<T> Where<T>(this ComandoSql<T> comando, Expression<Func<T, bool>> pred)
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
			comando.WhereText = resultado;
			return comando;
		}


		public static ComandoSql<T> OrderBy<T>(this ComandoSql<T> comando, Expression<Func<T, object>> pred, bool desc = false)
		{
			var member = pred.Body as MemberExpression ?? (pred.Body as UnaryExpression)?.Operand as MemberExpression;
			var prop = member?.Member as PropertyInfo;
			var nombreProp = (prop?.GetCustomAttribute(typeof(Columna)) as Columna)?.Nombre ?? prop?.Name ?? "";
			comando.OrderByText = $"{nombreProp} {(desc ? "DESC" : string.Empty)}";
			return comando;
		}


		public static ComandoSql<T> OrderBy<T>(this ComandoSql<T> comando, Expression<Func<T, object>> pred1, Expression<Func<T, object>> pred2, bool desc1 = false, bool desc2 = false)
		{
			var member1 = pred1.Body as MemberExpression ?? (pred1.Body as UnaryExpression)?.Operand as MemberExpression;
			var member2 = pred2.Body as MemberExpression ?? (pred2.Body as UnaryExpression)?.Operand as MemberExpression;
			var prop1 = member1?.Member as PropertyInfo;
			var nombreProp1 = (prop1?.GetCustomAttribute(typeof(Columna)) as Columna)?.Nombre ?? prop1?.Name ?? "";
			var prop2 = member2?.Member as PropertyInfo;
			var nombreProp2 = (prop2?.GetCustomAttribute(typeof(Columna)) as Columna)?.Nombre ?? prop2?.Name ?? "";
			comando.OrderByText = $"{nombreProp1} {(desc1 ? "DESC" : string.Empty)}, {nombreProp2} {(desc2 ? "DESC" : string.Empty)};";
			return comando;
		}








	}
}
