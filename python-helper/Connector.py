from bs4 import BeautifulSoup
import requests

from Credentials import CREDENTIALS

class Connector:
  def __init__(self):
    self.session = requests.session()
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