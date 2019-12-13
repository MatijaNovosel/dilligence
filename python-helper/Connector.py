from bs4 import BeautifulSoup
import requests
import pyodbc

from Credentials import CREDENTIALS

class Connector:
  def __init__(self):
    self.session = requests.session()
    self.token = None
    self.dbConnection = pyodbc.connect("Driver={SQL Server Native Client 11.0};Server=.;Database=tvz2;Trusted_Connection=yes;")
    self.dbCursor = self.dbConnection.cursor()
  def connect(self):
    response = self.session.get("https://moj.tvz.hr/")
    soupParser = BeautifulSoup(response.text, "lxml")
    token = soupParser.find("input", {
      "name": "TVZ"
    }).get("value")
    self.session.post("https://moj.tvz.hr/", {
      "TVZ": token,
      "login": CREDENTIALS["username"],
      "passwd": CREDENTIALS["password"]
    })
    self.token = token