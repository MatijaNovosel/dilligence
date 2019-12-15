from typing import Dict, List, Tuple

class Zaposlenik:
  def __init__(self, Ime, Prezime, TitulaIspred, TitulaIza, ImagePath, Email, VrstaZaposljenjaID, OdjelID):
    self.Ime = Ime
    self.Prezime = Prezime
    self.TitulaIspred = TitulaIspred
    self.TitulaIza = TitulaIza
    self.ImagePath = ImagePath
    self.Email = Email
    self.VrstaZaposljenjaID = VrstaZaposljenjaID
    self.OdjelID = OdjelID
  def __str__(self) -> str:
    return f"Ime: {self.Ime}, Prezime: {self.Prezime}, TitulaIspred: {self.TitulaIspred}, TitulaIza: {self.TitulaIza}"
  def __eq__(self, other) -> bool:
    return (isinstance(other, self.__class__) and self.__dict__ == other.__dict__)
  def __ne__(self, other) -> bool:
    return not self.__eq__(other)  

class Kolegij:
  def __init__(self, Ime: str, ECTS: int, KraticaSmjera: str, URL: str, Zaposlenici: List[Zaposlenik]):
    self.Ime = Ime
    self.ECTS = ECTS
    self.KraticaSmjera = KraticaSmjera
    self.URL = URL
  def __str__(self) -> str:
    return f"Ime: {self.Ime}, ECTS: {self.ECTS}, KraticaSmjera: {self.KraticaSmjera}, URL: {self.URL}"
  def __eq__(self, other) -> bool:
    return (isinstance(other, self.__class__) and self.__dict__ == other.__dict__)
  def __ne__(self, other) -> bool:
    return not self.__eq__(other)

class KolegijURL:
  def __init__(self, URL, KraticaSmjera):
    self.KraticaSmjera = KraticaSmjera
    self.URL = URL
  def __str__(self) -> str:
    return f"KraticaSmjera: {self.KraticaSmjera}, URL: {self.URL}"
  def __eq__(self, other) -> bool:
    return (isinstance(other, self.__class__) and self.__dict__ == other.__dict__)
  def __ne__(self, other) -> bool:
    return not self.__eq__(other)

class Student:
  def __init__(self, Ime, Prezime, JMBAG, Email, KraticaSmjera):
    self.Ime = Ime
    self.Prezime = Prezime
    self.JMBAG = JMBAG
    self.Email = Email
    self.KraticaSmjera = KraticaSmjera
  def __str__(self) -> str:
    return f"Ime: {self.Ime}, Prezime: {self.Prezime}, JMBAG: {self.JMBAG}, KraticaSmjera: {self.KraticaSmjera}, Email: {self.Email}"
  def __eq__(self, other) -> bool:
    return (isinstance(other, self.__class__) and self.__dict__ == other.__dict__)
  def __ne__(self, other) -> bool:
    return not self.__eq__(other)