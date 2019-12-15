from Connector import Connector
from Models import Kolegij
from Models import Student
from Models import KolegijURL
from Constants import KRATICE_SMJEROVA
from bs4 import BeautifulSoup
from typing import Dict, List, Tuple

class Manager:
  def __init__(self):
    self.connector = Connector()
    self.connector.connect()
    
  def getKolegijURLList(self) -> List[str]:
    URLList = []
    
    for kratica in KRATICE_SMJEROVA["REDOVNO"].values():
      URL = f"https://moj.tvz.hr/studij{kratica}/prikaz/mojpred?TVZ={self.connector.token}"
      response = self.connector.session.get(URL)
      response.encoding = response.apparent_encoding
      text = response.text.replace("è", "č").replace("æ", "ć").replace("Æ", "Ć")

      soup = BeautifulSoup(text, "lxml")
      select = soup.find("select", {
        "name": "nesto1"
      })

      selectValues = [ KolegijURL(x["value"], kratica) for x in select.find_all("option")]

      URLList.extend(selectValues)

    return URLList
    
  def getKolegijList(self) -> List[Kolegij]:
    kolegiji = []

    for kratica in KRATICE_SMJEROVA["REDOVNO"].values():
      URL = f"https://moj.tvz.hr/studij{kratica}/prikaz/mojpred?TVZ={self.connector.token}"
      response = self.connector.session.get(URL)
      response.encoding = response.apparent_encoding
      text = response.text.replace("è", "č").replace("æ", "ć").replace("Æ", "Ć")

      soup = BeautifulSoup(text, "lxml")
      select = soup.find("select", {
        "name": "nesto1"
      })

      selectValues = [ Kolegij(x.text, None, kratica, x["value"]) for x in select.find_all("option") ]

      kolegiji.extend(selectValues)

    return kolegiji
    
  def getStudentList(self, kolegijURLList: List[KolegijURL], kraticaSmjera: str = "inf") -> List[Student]:
    kolegijURLList = list(filter(lambda x: (x.KraticaSmjera == kraticaSmjera), kolegijURLList))
    studenti = set()

    for KolegijURL in kolegijURLList:
      URL = f"https://moj.tvz.hr/studij{kraticaSmjera}/predmet/22418?TVZ={self.connector.token}"
      response = self.connector.session.post(URL, data = {
        "supporttype": "prij",
        "TVZ": self.connector.token
      })
      response.encoding = response.apparent_encoding
      text = response.text.replace("è", "č").replace("æ", "ć").replace("Æ", "Ć")

      soup = BeautifulSoup(text, "lxml")
      table = soup.find("table", {
        "id": "podaci"
      })
      
      try:
        tableRows = table.findAll("tr")
        for tableRow in tableRows[1:]:
          rowCells = [ 
            x.text 
            for x in tableRow.findAll("td")[2:-1] 
            if x.text not in [ 'Organizacija i informatizacija ureda', 'Elektroničko poslovanje', 'Informatički dizajn' ] 
          ]
          student = Student(rowCells[1], rowCells[2], rowCells[0], rowCells[3], kraticaSmjera)
          studenti.add(student)
      except:
          continue

    return list(studenti)
    
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

    for student in studenti:
      self.connector.cursor.execute("INSERT INTO Student VALUES (?, ?, ?, ?, ?, ?)", 
        student.JMBAG, 
        student.Ime, 
        student.Prezime, 
        None, 
        None, 
        None
      )
      self.connector.commit()

manager = Manager()
studenti = manager.getStudentList(manager.getKolegijURLList())

for student in studenti:
  print(student)