namespace Quattro.Data
{
	using System;

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	public class Tabla : Attribute
	{
		public string Nombre { get; private set; }

		public Tabla(string nombre) => Nombre = nombre;

	}


	[AttributeUsage(AttributeTargets.Property)]
	public class Ignorar : Attribute { }


	[AttributeUsage(AttributeTargets.Property)]
	public class Columna : Attribute
	{
		public string Nombre { get; private set; }

		public Columna(string nombre) => Nombre = nombre;
	}


	[AttributeUsage(AttributeTargets.Property)]
	public class Clave : Attribute { }


	[AttributeUsage(AttributeTargets.Property)]
	public class NotNull : Attribute { }


	[AttributeUsage(AttributeTargets.Property)]
	public class Foranea : Attribute
	{
		public string ClaveForanea { get; private set; }
		public ForeignKeyActions OnDelete { get; set; } = ForeignKeyActions.NoAction;
		public ForeignKeyActions OnUpdate { get; set; } = ForeignKeyActions.NoAction;

		public Foranea(string claveForanea) => ClaveForanea = claveForanea;
	}
}
