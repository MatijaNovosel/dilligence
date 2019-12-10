from Connector import Connector
from Models import Kolegij
from Constants import KRATICE_SMJEROVA
from bs4 import BeautifulSoup
from typing import Dict, List, Tuple

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

manager = Manager()
manager.getKolegijList()