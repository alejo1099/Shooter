using UnityEngine;

public class MovimientoRotacion
{
    //Scripts encargado del movimiento y rotacion del jugador a traves del rigidbody

    private Transform transformPlayer;
    private Transform transformCamara;

    private Rigidbody rigidbodyPlayer;
    public Vector3 movimiento;

    private float velocidadMovimiento;
    private float velocidadRotacion;
    private float guardarVelocidadRotacion;


    //Contructor que pide las variables y referencias necesarias para iniciar el script
    public MovimientoRotacion(float velocidadMovimiento, float velocidadRotacion, Transform transformPlayer, Transform transformCamara)
    {
        this.velocidadMovimiento = velocidadMovimiento;
        this.velocidadRotacion = velocidadRotacion;
        guardarVelocidadRotacion = velocidadRotacion;
        this.transformPlayer = transformPlayer;
        rigidbodyPlayer = transformPlayer.GetComponent<Rigidbody>();
        this.transformCamara = transformCamara;
    }

    //Encargado de mover el jugador mediante su rigidbody
    public void MovimientoRigidbody()
    {
        movimiento = (transformPlayer.forward * Input.GetAxisRaw("Vertical") + transformPlayer.right * Input.GetAxisRaw("Horizontal")).normalized * velocidadMovimiento;

        rigidbodyPlayer.MovePosition(transformPlayer.position + movimiento * Time.fixedDeltaTime);
    }

    //Encargado de rotar al jugador por medio de su rigidbody, y a la camara por su Transform, ademas asegurandose de ponerle un limite a esta
    public void RotacionRigidbody()
    {
        rigidbodyPlayer.MoveRotation(transformPlayer.rotation * Quaternion.Euler(0f, Input.GetAxis("Mouse X") * velocidadRotacion, 0f));

        transformCamara.localRotation *= Quaternion.Euler(Input.GetAxis("Mouse Y") * -velocidadRotacion, 0f, 0f);

        velocidadRotacion = (transformCamara.localRotation.x >= 0.3826f && Input.GetAxis("Mouse Y") < -0.1f)
         || (transformCamara.localRotation.x <= -0.5f && Input.GetAxis("Mouse Y") > 0.1f) ? 0 : guardarVelocidadRotacion;
    }
}