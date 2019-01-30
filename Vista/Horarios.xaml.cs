using AlphaSport.Controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AlphaSport.Vista
{
    /// <summary>
    /// Lógica de interacción para Horarios.xaml
    /// </summary>
    public partial class Horarios : Window
    {
        private Entorno entorno;

        private UInt64 codigo;
        private string email;
        private List<string> diasInscritos = new List<string>();
        private List<string> horasInscritos = new List<string>();
        private List<string> diasModifi = new List<string>();
        private List<string> horasModifi = new List<string>();
        private bool bloqueadoEstu = false;

        private List<string> cuposL = new List<string>();
        private List<Label> labelsY = new List<Label>();        

        public Horarios()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance();
            lab1.Content = entorno.PROYECTO;

            IniciarLabelsY(); // crear Lista de Labels para mostrar cupos
            MostrarCupos(); // mostar cupos disponibles
            ActualizarCmbox(); // inicializa los Cmbox

            Limpiar();
        }

        /// <summary>
        /// COLOCA LOS VALORES DE DIAS Y HORAS DEL HORARIO DEL ESTUDIANTE EN LOS COMBOBOX
        /// </summary>
        /// <param name="dia1">DIA DE LA SEMANA</param>
        /// <param name="dia2">DIA DE LA SEMANA</param>
        /// <param name="dia3">DIA DE LA SEMANA</param>
        /// <param name="hora1">HORA DE LA SEMANA</param>
        /// <param name="hora2">HORA DE LA SEMANA</param>
        /// <param name="hora3">HORA DE LA SEMANA</param>
        private void HorarioCmbox(string dia1, string dia2, string dia3, string hora1, string hora2, string hora3)
        {
            cmbx1.SelectedValue = dia1;
            cmbx2.SelectedValue = dia2;
            cmbx3.SelectedValue = dia3;
            cmbx4.SelectedValue = hora1;
            cmbx5.SelectedValue = hora2;
            cmbx6.SelectedValue = hora3;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }            
             
        private void Limpiar()
        {   
            txt3.Text = "";
            txt4.Content = "------";
            txt5.Content = "------";
            txt6.Content = "--";
            txt7.Content = "--";
            txt14.Content = "---";
            txt8.Content = "Dia 1";
            txt9.Content = "Dia 2";
            txt10.Content = "Dia 3";
            txt11.Content = "N/A";
            txt12.Content = "N/A";
            txt13.Content = "N/A";

            cmbx1.SelectedValue = null;
            cmbx2.SelectedValue = null;
            cmbx3.SelectedValue = null;
            cmbx4.SelectedValue = null;
            cmbx5.SelectedValue = null;
            cmbx6.SelectedValue = null;

            diasInscritos = new List<string>();
            horasInscritos = new List<string>();
            diasModifi = new List<string>();
            horasModifi = new List<string>();
            bloqueadoEstu = false;

            EstadoCmboxDias(false);
            EstadoCmboxHoras(false);
            EstadosBotones(false);
            EstadosInicio(true);

            txt3.Focus();
        }

        private void EstadosInicio(bool estado)
        {
            txt3.IsEnabled = estado;
            bt5.IsEnabled = !estado;
        }

        private void EstadoCmboxDias(bool estado)
        {
            cmbx1.IsEnabled = estado;
            cmbx2.IsEnabled = estado;
            cmbx3.IsEnabled = estado;
        }

        private void EstadoCmboxHoras(bool estado)
        {
            cmbx4.IsEnabled = estado;
            cmbx5.IsEnabled = estado;
            cmbx6.IsEnabled = estado;
        }

        /// <summary>
        /// Cambia la visibilidad del input de codigo y el boton buscar manteniendolos contrarios al boton de modificar horario.
        /// </summary>
        /// <param name="estado">estado que tomarán los botones</param>
        private void EstadosBotones(bool estado)
        {   
            bt3.IsEnabled = !estado;
            txt3.IsEnabled = !estado;
            bt_mod.IsEnabled = estado;
            bt4.IsEnabled = estado;            
        }

        private void Click_bt2(object sender, RoutedEventArgs e)
        {
            Window tabla = new TablaInscritos();

            tabla.Show();
            this.Hide();
        }

        private void Click_bt1(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void Click_bt3(object sender, RoutedEventArgs e)
        {
            string varIn = txt3.Text;
            Limpiar();

            if (varIn == "" || (!UInt64.TryParse(varIn, out UInt64 abc)))
            {
                MessageBox.Show("Ingrese un codigo valido!");                
            }
            else
            {
                codigo = Convert.ToUInt64(varIn);

                EstadosInicio(false); // desactiva campo input, activa Bloqueos

                //0: nombre, 1: carrera, 2: email, 3: semestre, 4:  fallas, 5: asistencias, 6: dia1,hora1,dia2,hora2,dia3,hora3
                List<string> lista = entorno.Asistencia(codigo);

                List<string> bloqueado = entorno.EstuBloqueado(codigo);

                if (bloqueado.Count != 0 && (bloqueado[0] == entorno.ERRORSQL || lista[0] == entorno.INFOSQL))
                {
                    bloqueadoEstu = true;
                    MessageBox.Show(bloqueado[1]);
                }                
                
                bool buscarEstudiante = entorno.BuscarEstudiante(codigo, "");

                if (buscarEstudiante && lista.Count > 0)
                {
                    SepararDias(lista);  // descompone la posicion 5 y agrega los 6 elementos que se necesitan en el orden que se necesitan     

                    foreach (var item in lista)
                    {
                        Debug.WriteLine("<<<< Lista horario: " + item);
                    }

                    email = lista[2]; // GUARDA EMAIL

                    txt4.Content = lista[0];
                    txt5.Content = lista[1];
                    txt6.Content = lista[3];
                    txt7.Content = lista[4];
                    txt14.Content = lista[5];

                    txt8.Content = lista[6];
                    txt9.Content = lista[7];
                    txt10.Content = lista[8];
                    txt11.Content = lista[9];
                    txt12.Content = lista[10];
                    txt13.Content = lista[11];

                    HorarioCmbox(lista[6], lista[7], lista[8], lista[9], lista[10], lista[11]); // mostrar el horario en los Cmbox

                    // LLENAR LISTA DE DATOS DIAS Y HORAS
                    diasInscritos.Add(lista[6]);
                    diasInscritos.Add(lista[7]);
                    diasInscritos.Add(lista[8]);

                    horasInscritos.Add(lista[9]);
                    horasInscritos.Add(lista[10]);
                    horasInscritos.Add(lista[11]);

                    foreach (var item in diasInscritos)
                    {
                        Debug.WriteLine("--- IN dias inscritos: " + item + " email: " + email);
                    }

                    foreach (var item in diasInscritos)
                    {
                        Debug.WriteLine("--- IN horas inscritos: " + item + " email: " + email);
                    }

                    //CAMBIAR ESTADO DE LOS BOTONES
                    if (bloqueadoEstu)
                    {
                        EstadosBotones(false);
                        EstadoCmboxDias(false);
                        EstadoCmboxHoras(false);
                    }
                    else
                    {
                        EstadosBotones(true);
                        EstadoCmboxDias(true);
                        EstadoCmboxHoras(false);
                    }
                }
                else if (buscarEstudiante && lista.Count == 0)
                {
                    //0: nombre, 1: carrera, 2: email, 3: semestre, 4: fallas, 5: asistencias, 6: codigo
                    lista = entorno.DatosEstudiante(codigo);

                    MessageBox.Show("No esta registrado el Estudiante en el Gimnasio!");
                    if (lista.Count > 0)
                    {
                        foreach (var item in lista)
                        {
                            Debug.WriteLine("<<<< Lista datos estu: " + item);
                        }

                        email = lista[2]; // GUARDA EMAIL

                        txt4.Content = lista[0];
                        txt5.Content = lista[1];
                        txt6.Content = lista[3];
                        txt7.Content = lista[4];
                        txt14.Content = lista[5];

                        txt8.Content = "N/A";
                        txt9.Content = "N/A";
                        txt10.Content = "N/A";
                        txt11.Content = "N/A";
                        txt12.Content = "N/A";
                        txt13.Content = "N/A";

                        HorarioCmbox(null, null, null, "N/A", "N/A", "N/A"); // mostrar el horario en los Cmbox

                        // LLENAR LISTA DE DATOS DIAS Y HORAS
                        diasInscritos.Add("N/A");
                        diasInscritos.Add("N/A");
                        diasInscritos.Add("N/A");

                        horasInscritos.Add("N/A");
                        horasInscritos.Add("N/A");
                        horasInscritos.Add("N/A");

                        foreach (var item in diasInscritos)
                        {
                            Debug.WriteLine("--- NE dias inscritos: " + item + " email: " + email);
                        }

                        foreach (var item in diasInscritos)
                        {
                            Debug.WriteLine("--- NE horas inscritos: " + item + " email: " + email);
                        }
                       
                        //CAMBIAR ESTADO DE LOS BOTONES
                        if (bloqueadoEstu)
                        {
                            EstadosBotones(false);
                            EstadoCmboxDias(false);
                            EstadoCmboxHoras(false);
                        }
                        else
                        {
                            EstadosBotones(true);
                            EstadoCmboxDias(true);
                            EstadoCmboxHoras(false);
                        }

                        // PARA CREAR HORARIO DE ESTUDIANTE SIN REGISTRO
                    }
                }
                else
                {
                    Limpiar();
                    MessageBox.Show("No esta registrado en la Base de Datos de Deportes!");
                }
                                              
            }
        }

        private void SepararDias(List<string> lista)
        {
            // cambiar posicion 6 de lista: "d1,h1,d2,h2,d3,h3" por ["d1","d2","d3","h1","h2","h3"]

            string cadena = lista[6];
            lista.RemoveAt(6);

            if (cadena == "" || cadena == "N/A" || cadena.Length == 0)
            {
                lista.Add("N/A"); //d1
                lista.Add("N/A"); //d2
                lista.Add("N/A"); //d3

                lista.Add("N/A"); //h1
                lista.Add("N/A"); //h2
                lista.Add("N/A"); //h3
            }

            else
            {
                string[] separadas;
                separadas = cadena.Split(',');

                if (separadas.Length == 2 && separadas.Length > 0)
                {
                    Debug.WriteLine("<<< case  2,0");
                    lista.Add(separadas[0]);
                    lista.Add("N/A"); //d2
                    lista.Add("N/A"); //d3

                    lista.Add(separadas[1]);
                    lista.Add("N/A"); //h2
                    lista.Add("N/A"); //h3

                }
                else if (separadas.Length == 4 && separadas.Length > 2)
                {
                    Debug.WriteLine("<<< case  4,2");
                    lista.Add(separadas[0]);
                    lista.Add(separadas[2]);
                    lista.Add("N/A"); //d3

                    lista.Add(separadas[1]);
                    lista.Add(separadas[3]);
                    lista.Add("N/A"); //h3

                }
                else
                {
                    Debug.WriteLine("<<< case  all");
                    lista.Add(separadas[0]); //d1
                    lista.Add(separadas[2]); //d2
                    lista.Add(separadas[4]); //d3

                    lista.Add(separadas[1]); //h1
                    lista.Add(separadas[3]); //h2
                    lista.Add(separadas[5]); //h3
                }
            }

        }


        private List<string> SepararIds(List<string> lista)
        {
            // a,b,c,...,x  ==> {id, Lunes, Martes, Miercoles, Jueves, Viernes}

            List<string> result = new List<string>();

            string[] separadas;
            separadas = lista[0].Split(',');

            foreach (var item in separadas)
            {
                result.Add(item);
                Debug.WriteLine("<< a lista: " + item);
            }
            Debug.WriteLine("<< size : " + result.Count());
            return result;
        }


        private void Bt_mod_Click(object sender, RoutedEventArgs e)
        {
            //ModificarHorario();
            //cuposL = SepararIds(entorno.Cupos());
            //ActualizarCmbox();
        }

        /// <summary>
        /// MOFICA O ELIMINA UNO DE LOS DIAS DEL HORARIO DEL ESTUDIANTE
        /// </summary>
        /// <param name="dia">DIA DE LA SEMANA</param>
        /// <param name="hora">HORA DEL DIA DE LA SEMANA</param>
        /// <returns></returns>
        private string Modifica_dia_hora(string dia, int hora)
        {
            return "";
        }
       
        private void ModificarHorario()
        {
            string mensajeR = ""; // mensaje principal

            // DETERMINAR QUE SI CAMBIA EL DIA




            // declaración de variables
            string dia1 = "";
            int hora1 = 0;

            string dia2 = "";
            int hora2 = 0;

            string dia3 = "";
            int hora3 = 0;

            //verificacion de valores {1-4, 2-5, 3-6}
            if (cmbx1.SelectedValue != null)
            {
                if (cmbx4.SelectedValue != null)
                {
                    bool es_int = UInt32.TryParse(cmbx4.SelectedValue.ToString(), out UInt32 abc); // se puede convertir a Int (dia de la semana)

                    if (es_int) // es un dia de la semana -> modificar horario
                    { }
                    else // es N/A -> borrar horario
                    { }

                }
            }
            else
            {
                MessageBox.Show("Selección invalida!");
            }        

            dia1 = Convert.ToString(cmbx1.SelectedValue);
            hora1 = Convert.ToInt32(cmbx4.SelectedValue);

            dia2 = Convert.ToString(cmbx1.SelectedValue);
            hora2 = Convert.ToInt32(cmbx4.SelectedValue);

            dia3 = Convert.ToString(cmbx1.SelectedValue);
            hora3 = Convert.ToInt32(cmbx4.SelectedValue);

            string result = entorno.CambiarHorario(hora1, dia1, email);

            if (result == "")
            {
                MessageBox.Show("Agregado correctamnente!");
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void ActualizarCmbox()
        {        
            cmbx1.ItemsSource = entorno.DIAS;
            cmbx2.ItemsSource = entorno.DIAS;
            cmbx3.ItemsSource = entorno.DIAS;

            cmbx4.ItemsSource = entorno.HORAS;
            cmbx5.ItemsSource = entorno.HORAS;
            cmbx6.ItemsSource = entorno.HORAS;
        }

        private void MostrarCupos()
        {
            List<string> lista = entorno.Cupos();

            if (lista.Count != 0 && (lista[0] == entorno.ERRORSQL || lista[0] == entorno.INFOSQL))
            {
                MessageBox.Show("Error critico leyendo la base de datos!"+"\n"+"Contacte con el administrador:"+"\n"+ lista[1]);
                Application.Current.Shutdown();
            }

            cuposL = SepararIds(lista);

            labY0.Content = cuposL[0];
            labY1.Content = cuposL[1];
            labY2.Content = cuposL[2];
            labY3.Content = cuposL[3];
            labY4.Content = cuposL[4];
            labY5.Content = cuposL[5];

            labY6.Content = cuposL[6];
            labY7.Content = cuposL[7];
            labY8.Content = cuposL[8];
            labY9.Content = cuposL[9];
            labY10.Content = cuposL[10];
            labY11.Content = cuposL[11];

            labY12.Content = cuposL[12];
            labY13.Content = cuposL[13];
            labY14.Content = cuposL[14];
            labY15.Content = cuposL[15];
            labY16.Content = cuposL[16];
            labY17.Content = cuposL[17];

            labY18.Content = cuposL[18];
            labY19.Content = cuposL[19];
            labY20.Content = cuposL[20];
            labY21.Content = cuposL[21];
            labY22.Content = cuposL[22];
            labY23.Content = cuposL[23];

            labY24.Content = cuposL[24];
            labY25.Content = cuposL[25];
            labY26.Content = cuposL[26];
            labY27.Content = cuposL[27];
            labY28.Content = cuposL[28];
            labY29.Content = cuposL[29];

            labY30.Content = cuposL[30];
            labY31.Content = cuposL[31];
            labY32.Content = cuposL[32];
            labY33.Content = cuposL[33];
            labY34.Content = cuposL[34];
            labY35.Content = cuposL[35];

            labY36.Content = cuposL[36];
            labY37.Content = cuposL[37];
            labY38.Content = cuposL[38];
            labY39.Content = cuposL[39];
            labY40.Content = cuposL[40];
            labY41.Content = cuposL[41];

            labY42.Content = cuposL[42];
            labY43.Content = cuposL[43];
            labY44.Content = cuposL[44];
            labY45.Content = cuposL[45];
            labY46.Content = cuposL[46];
            labY47.Content = cuposL[47];

            labY48.Content = cuposL[48];
            labY49.Content = cuposL[49];
            labY50.Content = cuposL[50];
            labY51.Content = cuposL[51];
            labY52.Content = cuposL[52];
            labY53.Content = cuposL[53];
        }

        private void IniciarLabelsY()
        {
            // agregar Labels que muestran el horario
            labelsY.Add(labY0);
            labelsY.Add(labY1);
            labelsY.Add(labY2);
            labelsY.Add(labY3);
            labelsY.Add(labY4);
            labelsY.Add(labY5);
            // -----
            labelsY.Add(labY6);
            labelsY.Add(labY7);
            labelsY.Add(labY8);
            labelsY.Add(labY9);
            labelsY.Add(labY10);
            labelsY.Add(labY11);
            // -----
            labelsY.Add(labY12);
            labelsY.Add(labY13);
            labelsY.Add(labY14);
            labelsY.Add(labY15);
            labelsY.Add(labY16);
            labelsY.Add(labY17);
            // -----
            labelsY.Add(labY18);
            labelsY.Add(labY19);
            labelsY.Add(labY20);
            labelsY.Add(labY21);
            labelsY.Add(labY22);
            labelsY.Add(labY23);
            // -----
            labelsY.Add(labY24);
            labelsY.Add(labY25);
            labelsY.Add(labY26);
            labelsY.Add(labY27);
            labelsY.Add(labY28);
            labelsY.Add(labY29);
            // -----
            labelsY.Add(labY30);
            labelsY.Add(labY31);
            labelsY.Add(labY32);
            labelsY.Add(labY33);
            labelsY.Add(labY34);
            labelsY.Add(labY35);

            // -----
            labelsY.Add(labY36);
            labelsY.Add(labY37);
            labelsY.Add(labY38);
            labelsY.Add(labY39);
            labelsY.Add(labY40);
            labelsY.Add(labY41);
            // -----
            labelsY.Add(labY42);
            labelsY.Add(labY43);
            labelsY.Add(labY44);
            labelsY.Add(labY45);
            labelsY.Add(labY46);
            labelsY.Add(labY47);
            // -----
            labelsY.Add(labY48);
            labelsY.Add(labY49);
            labelsY.Add(labY50);
            labelsY.Add(labY51);
            labelsY.Add(labY52);
            labelsY.Add(labY53);
        }

        private void SeleccionCmbx(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmboxSelected = e.Source as ComboBox;
            string tag = cmboxSelected.Tag.ToString();
            
            if (tag == "1")
            {
                Debug.WriteLine("tag 1");
                cmbx4.IsEnabled = true;
            }
            else if (tag == "2")
            {
                Debug.WriteLine("tag 1");
                cmbx5.IsEnabled = true;
            }
            else if (tag == "3")
            {
                Debug.WriteLine("tag 1");
                cmbx6.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Ha ocurrido un error! tag no encontrado Horarios.xaml.cs");
            }           
        }

        private void Click_bt4(object sender, RoutedEventArgs e)
        {
            List<string> lista = entorno.EliminarHorario(email);
            string mensaje = "";

            if (lista.Count != 0 && (lista[0] == entorno.ERRORSQL || lista[0] == entorno.INFOSQL))
            {
                mensaje = lista[1];
            }
            else
            {   
                mensaje = "Horario Eliminado!";
            }


            MessageBox.Show(mensaje);

            Click_bt2(new object(), new RoutedEventArgs());
        }

        private void Click_bt5(object sender, RoutedEventArgs e)
        {
            
            string mensaje = "bloquear";
            if (bloqueadoEstu) mensaje = "desbloquear";            

            if (MessageBox.Show("Desea "+mensaje+" el estudiante?", mensaje+" a "+codigo, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            { // SI
                if (bloqueadoEstu)
                {
                    entorno.DesBloquearEstudiante(codigo);
                    bloqueadoEstu = true;
                }
                else
                {
                    entorno.BloquearEstudiante(codigo);
                    bloqueadoEstu = false;
                }
            }          

            Click_bt2(new object(), new RoutedEventArgs());

        }
    }
}
