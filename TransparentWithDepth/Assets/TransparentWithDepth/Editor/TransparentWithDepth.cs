using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor;

namespace BeautifulMaterials {
				
				public class BeautifulMaterials {

								static Material bmDepthOnly;


								[MenuItem ("GameObject/Depth Write/Add Depth Compatibility")]
								static void AddDepthOption () {
												Renderer renderer = GetRenderer ();
												if (renderer == null)
																return;

												Material[] materials = renderer.sharedMaterials;
												for (int k = 0; k < materials.Length; k++) {
																if (materials [k] == bmDepthOnly) {
																				EditorUtility.DisplayDialog ("Depth Support", "Already set! Nothing to do.", "Ok");
																				return;
																}
												}
												if (materials == null) {
																renderer.sharedMaterial = bmDepthOnly;
												} else {
																List<Material> newMaterials = new List<Material> (materials);
																newMaterials.Insert (0, bmDepthOnly);
																renderer.sharedMaterials = newMaterials.ToArray ();
												}
								}

								[MenuItem ("GameObject/Depth Write/Remove Depth Compatibility")]
								static void RemoveDepthOption () {

												Renderer renderer = GetRenderer ();
												if (renderer == null)
																return;

												Material[] materials = renderer.sharedMaterials;
												for (int k = 0; k < materials.Length; k++) {
																if (materials [k] == bmDepthOnly) {
																				List<Material> newMaterials = new List<Material> (renderer.sharedMaterials);
																				newMaterials.RemoveAt (k);
																				renderer.sharedMaterials = newMaterials.ToArray ();
																				return;
																}
												}

												for (int k = 0; k < materials.Length; k++) {
																if (materials [k] == bmDepthOnly) {
																				EditorUtility.DisplayDialog ("Depth Support", "This object was not previously modified! Nothing to do.", "Ok");
																				return;
																}
												}

								}


								static Renderer GetRenderer () {

												if (Selection.activeGameObject == null) {
																EditorUtility.DisplayDialog ("Depth Support", "This option can only be used on GameObjects.", "Ok");
																return null;
												}
												Renderer renderer = Selection.activeGameObject.GetComponent<Renderer> ();
												if (renderer == null) {
																EditorUtility.DisplayDialog ("Depth Support", "This option can only be used on GameObjects with a Renderer component attached.", "Ok");
																return null;
												}

												if (bmDepthOnly == null) {
																bmDepthOnly = Resources.Load<Material> ("BMaterials/Materials/BMJustDepth");
																if (bmDepthOnly == null) {
																				EditorUtility.DisplayDialog ("Depth Support", "BMJustDepth material not found!", "Ok");
																				return null;
																}
												}

												return renderer;
								}


				}
}