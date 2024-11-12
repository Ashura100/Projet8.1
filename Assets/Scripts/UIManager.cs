using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public RawImage vehicleImage;               // Image de rendu de la caméra
    public List<CarSO> carList = new List<CarSO>();  // Liste des véhicules disponibles
    public Transform[] spawnPoints;             // Tableau de points de pose
    private int currentVehicleIndex = 0;        // Index du véhicule actue

    private CarSO currentCar;                   // Référence à la voiture actuellement sélectionnée
    private List<GameObject> instantiatedCars = new List<GameObject>(); // Liste des véhicules instanciés

    void Start()
    {
        currentVehicleIndex = 0;
    }
    void Update()
    {
        NextVehicle();
        PreviousVehicle();
    }
    // Méthode pour changer le véhicule affiché
    public void SwitchVehicle(CarSO car)
    {
        // Définir la voiture actuelle
        currentCar = car;

        // Mettre à jour l'image avec la RenderTexture de la nouvelle voiture
        vehicleImage.texture = currentCar.image;

        // Activer le GameObject associé au véhicule sélectionné
        foreach (var carSO in carList)
        {
            if (carSO.gameOject != null)
                carSO.gameOject.SetActive(carSO == currentCar);
        }
    }

    // Méthode pour passer au véhicule suivant dans la liste
    public void NextVehicle()
    {
        currentVehicleIndex = (currentVehicleIndex + 1) % carList.Count;
        SwitchVehicle(carList[currentVehicleIndex]);
    }

    // Méthode pour passer au véhicule précédent dans la liste
    public void PreviousVehicle()
    {
        currentVehicleIndex = (currentVehicleIndex - 1 + carList.Count) % carList.Count;
        SwitchVehicle(carList[currentVehicleIndex]);
    }

    // Méthode pour changer la couleur en fonction de la couleur du bouton sélectionné
    public void ChangeColor(Button colorButton)
    {
        if (currentCar != null && currentCar.color != null)
        {
            // Récupérer la couleur de l'image du bouton
            Color selectedColor = colorButton.GetComponent<Image>().color;

            // Appliquer la couleur au matériau de la voiture actuelle
            currentCar.color.color = selectedColor;
        }
    }

    // Méthode pour sélectionner et instancier la voiture actuelle sur un point de pose
    public void CarSelected()
    {
        // Détruire tous les véhicules déjà instanciés pour éviter les doublons
        foreach (var car in instantiatedCars)
        {
            if (car != null)
                Destroy(car);
        }
        instantiatedCars.Clear();  // Vider la liste des véhicules instanciés

        // Instancier le véhicule sur chaque point de pose
        if (currentCar != null && currentCar.gameOject != null)
        {
            foreach (var spawnPoint in spawnPoints)
            {
                GameObject newCar = Instantiate(currentCar.gameOject, spawnPoint.position, spawnPoint.rotation);
                newCar.transform.SetParent(spawnPoint, true);
                instantiatedCars.Add(newCar);  // Ajouter à la liste des véhicules instanciés
            }
        }
    }
}