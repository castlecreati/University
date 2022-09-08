using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
	public class Curso: BaseEntity
	{
		[Required, StringLength(100)]
		public string Nombre { get; set; } = String.Empty;

		[Required,StringLength(280)]
		public string DescripcionCorta { get; set; } = String.Empty;
		public string? DescripcionLarga { get; set; } = String.Empty;
		public string? PublicoObjetivo { get; set; } = String.Empty;
		public string? Objetivos { get; set; } = String.Empty;
		public string? Requisitos { get; set; } = String.Empty;
		[Required]
		public Nivel Nivel { get; set; } 
	}

	public enum Nivel
	{
		Basico,
		Intermedio,
		Avanzado
	}
}
