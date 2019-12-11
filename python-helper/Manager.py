from Connector import Connector
from Models import Kolegij
from Models import Student
from Constants import KRATICE_SMJEROVA
from bs4 import BeautifulSoup
from typing import Dict, List, Tuple
import pyodbc

class Manager:
  def __init__(self):
    self.connector = Connector()
    self.connector.connect()
  def getKolegijList(self) -> List[Kolegij]:
    kolegiji = []

    # URL -> https://moj.tvz.hr/studij<KRATICA>/prikaz/mojpred?TVZ=<TOKEN>
    for kratica in KRATICE_SMJEROVA["REDOVNO"].values():
      response = self.connector.session.get(f"https://moj.tvz.hr/studij{kratica}/prikaz/mojpred?TVZ={self.connector.token}")
      response.encoding = response.apparent_encoding
      text = response.text.replace("è", "č")

      soup = BeautifulSoup(text, "lxml")
      select = soup.find("select", {
        "name": "nesto1"
      })

      selectValues = [ 
        Kolegij(x.text, None, kratica, x["value"])
        for x in select.find_all("option") 
        if x.get("value") not in ['120015', '0', '140010']
      ]

    return kolegiji
  def getStudentList(self, kraticaSmjera = "inf") -> List[Student]:
    studenti = [ Student("Matija", "Novosel", "0246073749", "INF") ]
    kolegiji = self.getKolegijList()

    return studenti
  def insertStudenti(self):
    '''

      [Student]
        ID
        JMBAG
        Ime
        Prezime
        ImagePath
        Email
        SmjerID

    '''
    studenti = self.getStudentList()

    dbConnection = pyodbc.connect("Driver={SQL Server Native Client 11.0};Server=.;Database=tvz2;Trusted_Connection=yes;")
    cursor = dbConnection.cursor()

    for student in studenti:
      cursor.execute("INSERT INTO Student VALUES (?, ?, ?, ?, ?, ?)", 
        student.JMBAG, 
        student.Ime, 
        student.Prezime, 
        None, 
        None, 
        None
      )
      cursor.commit()