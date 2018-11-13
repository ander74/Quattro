namespace Quattro.Data
{
	using System.Text;

	public class ComandoSql<T>
	{

		// ====================================================================================================
		#region CONSTRUCTORES
		// ====================================================================================================

		public ComandoSql() {  }


		public ComandoSql(string commandText) => CommandText = commandText;

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS
		// ====================================================================================================

		public string GetText()
		{
			var texto = new StringBuilder($"{CommandText} ");
			if (!string.IsNullOrEmpty(WhereText)) texto.Append($"\nWHERE {WhereText} ");
			if (!string.IsNullOrEmpty(OrderByText)) texto.Append($"\nORDER BY {OrderByText} ");
			if (!string.IsNullOrEmpty(GroupByText))
			{
				texto.Append($"\nGROUP BY {GroupByText} ");
				if (!string.IsNullOrEmpty(HavingText)) texto.Append($"\nHAVING {HavingText} ");
			}
			texto.Append(";");
			return texto.ToString();
		}


		public override string ToString()
		{
			var texto = new StringBuilder($"{CommandText} ");
			if (!string.IsNullOrEmpty(WhereText)) texto.Append($"WHERE {WhereText} ");
			if (!string.IsNullOrEmpty(OrderByText)) texto.Append($"ORDER BY {OrderByText} ");
			if (!string.IsNullOrEmpty(GroupByText))
			{
				texto.Append($"GROUP BY {GroupByText} ");
				if (!string.IsNullOrEmpty(HavingText)) texto.Append($"HAVING {HavingText} ");
			}
			texto.Append(";");
			return texto.ToString();
		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region PROPIEDADES
		// ====================================================================================================

		public string CommandText { get; set; }

		public string WhereText { get; set; }

		public string OrderByText { get; set; }

		public string GroupByText { get; set; }

		public string HavingText { get; set; }

		#endregion
		// ====================================================================================================

	}
}
