class Kolegij:
  def __init__(self, Ime, ECTS, KraticaSmjera, URL):
    self.Ime = Ime
    self.ECTS = ECTS
    self.KraticaSmjera = KraticaSmjera
    self.URL = URL
  def __str__(self) -> str:
    return f"Ime: {self.Ime}, ECTS: {self.ECTS}, KraticaSmjera: {self.KraticaSmjera}, URL: {self.URL}"