using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;  //SE REFERENCIO EL PROYECTO A VISUAL BASIC PARA SU USO CON ALGUNAS HERRAMIENTAS

namespace WFGestorEstudiantil
{
    public partial class FormGestion : Form
    {
        Datos estu; //VARIABLE DE TIPO Datos, SE USA EN LOS EVENTOS DE LOS BOTONES
        public FormGestion()
        {
            InitializeComponent();
            estu = new Datos();     // SE INICIALIZA LA VARIABLE estu
        }

        private void FormGestion_Load(object sender, EventArgs e)
        {

        }

        private void buttonAgregar_Click(object sender, EventArgs e) //METODO PARA DARLE FUNCIONALIDAD AL BOTON AGREGAR
        {
            try //USO DE TRY CATCH PARA CONTROLAR LAS EXCEPCIONES/ERRORES QUE PUEDA ARROJAR LA FUNCION DEL BOTON
            {   //SE USA Interaction.InputBox PARA MOSTRAR UNA VENTANA PARA QUE EL USUARIO INGRESE DATOS, USANDO LA VARIABLE estu PARA ACCEDER AL 
                //METODO AgregarEstudiante DE LA CLASE Datos
                estu.AgregarEstudiante(new Estudiantes(Interaction.InputBox("ID: "), Interaction.InputBox("Nombre: "), Interaction.InputBox("Ap Paterno: "), Interaction.InputBox("Ap Materno: "), Interaction.InputBox("Materia: "), Interaction.InputBox("Calificacion: "), Interaction.InputBox("Asistencia: ")));
                Mostrar(dataGridView1, estu.RetornarEstudiante());  //SE ACCEDE AL METODO Mostrar() 
            } 
            catch (Exception) //EN CASO DE LANZAR ERROR(EXCEPCION) SE EJECUTARÁ EL CODIGO DENTRO DE CATCH
            {
                throw;      //SEÑALA LA APARICION DE UNA EXCEPCION AL EJECUTAR EL PROGRAMA
            }
        }

        private void Mostrar(DataGridView pmostrardatos, object pobjeto)    //METODO PRIVADO PARA MOSTRAR LA LISTA EN EL DataGridView
        {
            pmostrardatos.DataSource = null;     //CON DataSource HACEMOS EL VINCULO CON EL FORMULARIO, ESTABLECE EL ORIGEN DE LOS DATOS
            pmostrardatos.DataSource = pobjeto;
        }

        private void buttonSalir_Click(object sender, EventArgs e) //METODO PRIVADO PARA FUNCION SALIR
        {
            Application.Exit(); //CIERRA EL FORMULARIO ACTUAL AL DAR CLICK EN EL BOTON CORRESPONDIENTE
        }

        private void buttonEliminar_Click(object sender, EventArgs e) //METODO PRIVADO PARA ELIMINAR ESTUDIANTES(FILAS) DE LA LISTA
        {
            try         //USO DE TRY CATCH PARA CONTROLAR LAS EXCEPCIONES/ERRORES QUE PUEDA ARROJAR LA FUNCION DEL BOTON
            {
                if (dataGridView1.Rows.Count > 0) // VERIFICA QUE dataGridView1 TENGA DATOS A ELIMINAR, QUE TENGA NUMERO DE FILAS MAYOR A CERO
                {
                    estu.EliminarEstudiante((dataGridView1.SelectedRows)[0].DataBoundItem as Estudiantes); //PARA LAS FILAS SELECCIONADAS EN EL dataGridView1
                                                                                                        //SE EJECUTA EL METODO EliminarEstudiante
                    Mostrar(dataGridView1, estu.RetornarEstudiante());      //EJECUTA METODO MOSTRAR PARA RETORNAR LOS ESTUDIANTES AL dataGridView1
                }
                else // AL NO CUMPLIR EL IF SE LANZA UNA EXCEPCION 
                {
                    throw new Exception("No Existen Estudiantes a Eliminar");   // EXCEPCION QUE MUESTRA MENSAJE
                }
            }
            catch (Exception ex)    //EN CASO DE LANZAR ERROR(EXCEPCION) SE EJECUTARÁ EL CODIGO DENTRO DE CATCH CON LA VARIABLE ex
            {
                MessageBox.Show(ex.Message); //SE RETORNA MENSAJE DE EXCEPCION
            }
        }

        private void buttonEditar_Click(object sender, EventArgs e)
        {
            try    //USO DE TRY CATCH PARA CONTROLAR LAS EXCEPCIONES/ERRORES QUE PUEDA ARROJAR LA FUNCION DEL BOTON
            {
                if (dataGridView1.Rows.Count > 0) //SE ESTABLECE QUE LAS FILAS DEL dataGridView1 DEBEN SER MAYOR QUE CERO
                {   //
                    Estudiantes editarestu = (dataGridView1.SelectedRows[0].DataBoundItem as Estudiantes);

                    //CADA PROPIEDAD DE LA LISTA SE ASIGNA NUEVO VALOR AGREGADO POR EL USUARIO POR MEDIO DE Interaction.InputBox
                    editarestu.Nombre = Interaction.InputBox("Ingrese Nombre: ", editarestu.Nombre);
                    editarestu.ApPaterno = Interaction.InputBox("Ingrese Ap Paterno: ", editarestu.ApPaterno);
                    editarestu.ApMaterno = Interaction.InputBox("Ingrese Ap Materno: ", editarestu.ApMaterno);
                    editarestu.Materia = Interaction.InputBox("Ingrese Materia: ", editarestu.Materia);
                    editarestu.Calificacion = Interaction.InputBox("Ingrese Calificacion: ", editarestu.Calificacion);
                    editarestu.Asistencia = Interaction.InputBox("Ingrese Asistencia: ", editarestu.Asistencia);
                    estu.EditarEstudiante(editarestu);  // SE LLAMA EL METODO EditarEstudiante CON PARAMETRO editarestu
                    Mostrar(dataGridView1, estu.RetornarEstudiante());  // SE LLAMA EL METODO Mostrar PARA ACTUALIZAR EL VALOR DE LA LISTA AL dataGridView1
                }
                else   // EN CASO DE NO CUMPLIRSE EL IF SE LANZARÁ UN MENSAJE A TRÁVES DE UNA EXCEPCION EXPLICITA
                {
                    throw new Exception("No Existen Estudiantes a Editar");    //EXCEPCION EXPLICITA
                }
            }
            catch(Exception ex)     //EN CASO DE LANZAR ERROR(EXCEPCION) SE EJECUTARÁ EL CODIGO DENTRO DE CATCH CON LA VARIABLE ex
            {
                MessageBox.Show(ex.Message);  //SE RETORNA MENSAJE DE LA EXCEPCION EXPLICITA
            }
        }
    }

    public class Datos      //CLASE PARA CONSTRUIR METODOS DEL FUNCIONAMIENTO DEL PROGRAMA
    {
        List<Estudiantes> datosEstudiantes;     //SE CREA LISTA TIPO Estudiantes Y SE ASIGNA A LA VARIABLE datosEstudiantes

        public Datos()      // METODO CONSTRUCTOR PARA TRANSFERIR LA LISTA DE LOS ESTUDIANTES
        {
            datosEstudiantes = new List<Estudiantes>();
        }

        public void AgregarEstudiante(Estudiantes pEstudiante)          //METODO PARA AGREGAR DATOS A LA LISTA, SE RECIBE UN PARAMETRO DE TIPO Estudiantes
        {                                                               //CON .Add SE AGREGAN ELEMENTOS A LA LISTA (DATOS DE LOS ESTUDIANTES)
            datosEstudiantes.Add(new Estudiantes(pEstudiante.Id, pEstudiante.Nombre, pEstudiante.ApPaterno, pEstudiante.ApMaterno, pEstudiante.Materia, pEstudiante.Calificacion, pEstudiante.Asistencia));
        }

        
        public List<Estudiantes> RetornarEstudiante() //METODO CON LA FUNCION DE RETORNAR ESTUDIANTES
        {
            List<Estudiantes> x = new List<Estudiantes>();      //SE CREA LISTA EXTRA DE TIPO Estudiantes CON LA FINALIDAD DE QUE EL USUARIO NO TENGA ACCESO A MANIPULAR DATOS AL USAR EL PROGRAMA

            foreach (Estudiantes i in datosEstudiantes)     //ITERA LA LISTA datosEstudiantes
            {
                x.Add(new Estudiantes(i.Id, i.Nombre, i.ApPaterno, i.ApMaterno, i.Materia, i.Calificacion, i.Asistencia));  //SE AGREGAN LOS ELEMENTOS DE LA LISTA A LA LISTRA EXTRA 'x'
            }

            return x;  //RETORNA EL VALOR DE LA LISTA CREADA
        }

        public void EliminarEstudiante(Estudiantes pEstudiante) //METODO PARA ELIMINAR DATOS DE LA LISTA, SE RECIBE UN PARAMETRO DE TIPO Estudiantes
        {
            try     //USO DE TRY CATCH PARA CONTROLAR LAS EXCEPCIONES/ERRORES
            {
                Estudiantes eliminarestud = datosEstudiantes.Find(x=> x.Id == pEstudiante.Id); //LA VARIABLE eliminarestud SE LE ASIGNA VALOR DE LA LISTA datosEstudiantes
                                                                                               //CUANDO SU ID SEA IGUAL AL ID DEL PARAMETRO pEstudiante
                if (eliminarestud != null)  //SE VERIFICA QUE NO ESTE VACIA LA VARIABLE PARA NO INTENTAR BORRAR DATOS QUE NO EXISTEN
                {
                    datosEstudiantes.Remove(eliminarestud); //CON .Remove SE ELIMINAN LOS DATOS DE LA LISTA datosEstudiantes
                }
            }
            catch (Exception)      //EN CASO DE LANZAR ERROR(EXCEPCION) SE EJECUTARÁ EL CODIGO DENTRO DE CATCH
            {
                throw;      //SEÑALA LA APARICION DE UNA EXCEPCION AL EJECUTAR EL PROGRAMA
            }

        }

        public void EditarEstudiante(Estudiantes pEstudiante)   //METODO PARA EDITAR DATOS DE LA LISTA, SE RECIBE UN PARAMETRO DE TIPO Estudiantes
        {
            try   //USO DE TRY CATCH PARA CONTROLAR LAS EXCEPCIONES/ERRORES
            {
                Estudiantes editarestud = datosEstudiantes.Find(x => x.Id == pEstudiante.Id);  //LA VARIABLE editarestud SE LE ASIGNA VALOR DE LA LISTA datosEstudiantes
                                                                                               //CUANDO SU ID SEA IGUAL AL ID DEL PARAMETRO pEstudiante
                {
                    if (editarestud != null) //EN CASO DE NO EXISTIR UN ESTUDIANTE
                    {   // A CADA PROPIEDAD DE LA LISTA datosEstudiantes SE LE ASIGNA EL VALOR DEL PARAMETRO pEstudiante
                        editarestud.Nombre = pEstudiante.Nombre;
                        editarestud.ApPaterno = pEstudiante.ApPaterno;
                        editarestud.ApMaterno = pEstudiante.ApMaterno;
                        editarestud.Materia = pEstudiante.Materia;
                        editarestud.Calificacion = pEstudiante.Calificacion;
                        editarestud.Asistencia = pEstudiante.Asistencia;
                    }
                    else  // EN CASO DE NO CUMPLIRSE EL IF SE LANZARÁ UN MENSAJE A TRÁVES DE UNA EXCEPCION EXPLICITA
                    {
                        throw new Exception("El Estudiante No Existe");     //EXCEPCION EXPLICITA
                    }
                }
                
            }
            catch(Exception ex)  //EN CASO DE LANZAR ERROR(EXCEPCION) SE EJECUTARÁ EL CODIGO DENTRO DE CATCH
            {
                throw new Exception(ex.Message);    ////SE RETORNA MENSAJE DE LA EXCEPCION EXPLICITA
            }
        }

    }

    public class Estudiantes //CLASE PARA LOS GET Y SET DE LOS ATRIBUTOS DE LOS ESTUDIANTES
    {
        public Estudiantes()
        {
        }

        //CONSTRUCTOR PARA ESTABLECER LAS PROPIEDADES
        public Estudiantes(string pId, string pNombre, string pApPaterno, string pApMaterno, string pMateria, string pCalificacion, string pAsistencia)
        {
            Id = pId;                   //INICIALIZA LAS PROPIEDADES DEL OBJETO ASIGNANDOLE SU RESPECTIVO VALOR DEL PARAMETRO
            Nombre = pNombre;
            ApPaterno = pApPaterno;
            ApMaterno = pApMaterno;
            Materia = pMateria;
            Calificacion = pCalificacion;
            Asistencia = pAsistencia;
        }

        //GET Y SET DE LAS PROPIEDADES (DATOS) DE LOS ESTUDIANTES(OBJETO)
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public string Materia { get; set; }
        public string Calificacion { get; set; }
        public string Asistencia { get; set; }

    }

}
