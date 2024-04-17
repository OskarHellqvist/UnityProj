using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
	public class opencloseWindowApt : MonoBehaviour
	{

		public Animator openandclosewindow;
		public bool open;
		public Transform Player;

		void Start()
		{
			open = false;
		}

		void OnMouseOver()
		{
			{
				if (Player)
				{
					float dist = Vector3.Distance(Player.position, transform.position);
					if (dist < 3)
					{
						if (open == false)
						{
							if (Input.GetMouseButtonDown(0))
							{
								StartCoroutine(opening());
							}
						}
						else
						{
							if (open == true)
							{
								if (Input.GetMouseButtonDown(0))
								{
									StartCoroutine(closing());
								}
							}

						}

					}
				}

			}

		}

		IEnumerator opening()
		{
			print("you are opening the Window");
			openandclosewindow.Play("Openingwindow");
			yield return new WaitForSeconds(.5f);
			open = true;
		}

		IEnumerator closing()
		{
			print("you are closing the Window");
			openandclosewindow.Play("Closingwindow");
			yield return new WaitForSeconds(.5f);
			open = false;
		}


	}
}