using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace WFGestorEstudiantil
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonAceptar_Click(object sender, EventArgs e) //MÉTODO DE EVENTO POR CLICK EN BOTON ACEPTAR PARA VALIDAR INICIO DE SESION
        {
            string usuario, contrasenia;    //VARIABLES PARA GUARDAR DATOS DE INICIO DE SESION INTRODUCIDOS DESDE TEXTBOX
            usuario = textBoxUsuario.Text.Trim();
            contrasenia = textBoxContra.Text.Trim();

                //IF PARA VALIDAR QUE CADA USUARIO CORRESPONDE CON SU CONTRASENIA
            if ((usuario.Equals("natha") && contrasenia.Equals("12345") || (usuario.Equals("nico") && contrasenia.Equals("uwu3000")) || 
                usuario.Equals("liz") && contrasenia.Equals("123456") || usuario.Equals("alex") && contrasenia.Equals("1234567")))
            {
                this.Hide();    // OCULTA EL FORMULARIO ACTUAL
                FormGestion op = new FormGestion(); // SE CREA OBJETO DEL FORMULARIO A UTILIZAR
                op.Show();  // MUESTRA EL FORMULARIO FormGestion
            }
            else // SI LA VALIDACION DE USUARIO NO ES CORRECTA SE MUESTRA UN MENSAJE
            {
                MessageBox.Show("¡Usuario y/o contraseña incorrectos!");
            }
        }

        private void buttonSalir_Click(object sender, EventArgs e) //MÉTODO DE EVENTO POR CLICK EN BOTON SALIR, PARA CERRAR EL FORMULARIO
        {
            Application.Exit(); //CIERRA EL FORMULARIO ACTUAL
        }

        private void buttonLimpiar_Click(object sender, EventArgs e) //MÉTODO DE EVENTO POR CLICK EN BOTON LIMPIAR, PARA BORRAR CAMPOS EN TEXTBOX
        {
            textBoxContra.Clear();      //BORRA LOS CAMPOS EN LOS TEXTBOX DE CONTRASENIA Y USUARIO
            textBoxUsuario.Clear();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
