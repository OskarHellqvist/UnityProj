﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
	public class opencloseDoor : MonoBehaviour
	{

		public Animator openandclose;
		public bool open;
		public Transform Player;

		public bool locked = false;

		void Start()
		{
			open = false;
		}

		void OnMouseOver()
		{
			if (Player)
			{
				float dist = Vector3.Distance(Player.position, transform.position);
				if (dist < 3)
				{

					if (open == false && !locked)
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

		IEnumerator opening()
		{
			print("you are opening the door");
			openandclose.Play("Opening");
			FindObjectOfType<AudioManager>().Play("Door");
			yield return new WaitForSeconds(.5f);
			open = true;
		}

		IEnumerator closing()
		{
			print("you are closing the door");
			openandclose.Play("Closing");
			yield return new WaitForSeconds(.5f);
			open = false;
		}


	}
}