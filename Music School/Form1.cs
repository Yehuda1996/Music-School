using static Music_School.Service.MusicSchoolService;

namespace Music_School
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateXMLIfNotExist();
            //InsertClassroom("guitar jazz");
            //AddTeacher("guitar jazz", "yossi levi");
            //AddStudent("guitar jazz", "freddy mercury", "guitar");
        }
    }
}
