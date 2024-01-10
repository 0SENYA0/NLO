using System;
using UnityEngine;

namespace DefaultNamespace
{
	public class VectorToAngle : MonoBehaviour
	{
		[SerializeField] private float _rotationAngle = 45f;

		private void Start()
		{
			// Переводим угол из градусов в радианы
			float angleToRadians = _rotationAngle * Mathf.Deg2Rad;
			
			// Создаём комплесное число, представляющее поворот на угол angleToRadians
			ComplexNumber rotationComplex = new ComplexNumber(Mathf.Cos(angleToRadians), Mathf.Sin(angleToRadians));
			
			// Создаём вектор Vector2, который нужно повернуть
			Vector2 vectorToRotate = new Vector2(1f, 0f);
			
			// Представляем вестор Vector2 в виде комплексного числа
			ComplexNumber vectorComplex = new ComplexNumber(vectorToRotate.x, vectorToRotate.y);
			
			// Умножаем комплексные числа (поворота и представления вектора) // один раз повернут на заданный угол
			// rotationComplex * rotationComplex * vectorComplex - два раза повернёт на заданный угол
			ComplexNumber resultComplex = rotationComplex * vectorComplex;
			
			// Преобразуем результирующий комплексный вектор обратно в Vector2
			Vector2 rotatedVector = new Vector2(resultComplex.Real, resultComplex.Imaginary);
			
			Debug.Log("Повернутый вектор = " + rotatedVector);
		}
	}

	internal class ComplexNumber
	{
		private readonly float _real;
		private readonly float _imaginary;

		public ComplexNumber(float real, float imaginary)
		{
			_real = real;
			_imaginary = imaginary;
		}

		public float Real => _real;

		public float Imaginary => _imaginary;

		public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
		{
			float realPart = a._real * b._real - a._imaginary * b._imaginary;
			float imaginary = a._real * b._imaginary + a._imaginary * b._real;

			return new ComplexNumber(realPart, imaginary);
		}
	}
}