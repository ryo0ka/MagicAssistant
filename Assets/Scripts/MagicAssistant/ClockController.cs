using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace MagicAssistant
{
	[ExecuteInEditMode]
	public class ClockController : MonoBehaviour
	{
		[SerializeField]
		Text _hhmmText;

		[SerializeField]
		Text _ssText;

		[SerializeField]
		Text _ampmText;

		[SerializeField]
		Text _weekdayText;

		[SerializeField]
		Text _mmddText;
		
		[SerializeField] 
		bool _suppress;

		void Update()
		{
			if (_suppress) return;
			
			DateTime dt = DateTime.Now;
			_hhmmText.text = $"{dt.Hour:00}:{dt.Minute:00}";
			_ssText.text = $"{dt.Second:00}";
			_ampmText.text = dt.ToString("tt", new CultureInfo("en-US"));
			_weekdayText.text = Weekday(dt);
			_mmddText.text = $"{dt.Month: 0}/{dt.Day:00}";
		}

		string Weekday(DateTime dt)
		{
			switch (dt.DayOfWeek)
			{
				case DayOfWeek.Friday: return "Fri.";
				case DayOfWeek.Monday: return "Mon.";
				case DayOfWeek.Saturday: return "Sat.";
				case DayOfWeek.Sunday: return "Sun.";
				case DayOfWeek.Thursday: return "Thu.";
				case DayOfWeek.Tuesday: return "Tue.";
				case DayOfWeek.Wednesday: return "Wed.";
				default: throw new ArgumentOutOfRangeException();
			}
		}
	}
}