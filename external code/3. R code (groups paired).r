# Lista di matrici di contingenza
matrici_contingenza <- list(
  Education_2x1 = matrix(c(8, 24, 14, 0, 7, 0, 0, 0, 0, 11, 1, 18, 15, 8), nrow = 7, byrow = FALSE),
  Education_2x3 = matrix(c(8, 24, 14, 0, 7, 0, 0, 0, 12, 31, 0, 9, 2, 0), nrow = 7, byrow = FALSE),
  Education_1x3 = matrix(c(0, 0, 0, 12, 11, 31, 1, 0, 18, 9, 15, 2, 8, 0), nrow = 7, byrow = TRUE),
  Affective_Symptoms_2x1 = matrix(c(0, 3, 0, 4, 0, 8, 0, 11, 0, 5, 0, 6, 0, 0, 0, 5, 0, 1, 1, 5, 0, 3, 5, 1, 15, 0, 0, 0, 9, 0, 23, 1), nrow = 16, byrow = TRUE),
  Affective_Symptoms_2x3 = matrix(c(0, 0, 0, 0, 0, 0, 0, 1, 0, 4, 0, 5, 0, 1, 0, 6, 0, 0, 1, 8, 0, 10, 5, 6, 15, 1, 0, 1, 9, 9, 23, 2), nrow = 16, byrow = TRUE),
  Affective_Symptoms_1x3 = matrix(c(3, 0, 4, 0, 8, 0, 11, 1, 5, 4, 6, 5, 0, 1, 5, 6, 1, 0, 5, 8, 3, 10, 1, 6, 0, 1, 0, 1, 0, 9, 1, 2), nrow = 16, byrow = TRUE),
  Rumination_2x1 = matrix(c(0, 6, 0, 9, 0, 7, 0, 7, 0, 9, 1, 8, 2, 6, 13, 0, 10, 0, 27, 1), nrow = 10, byrow = TRUE),
  Rumination_2x3 = matrix(c(0, 1, 0, 0, 0, 1, 0, 2, 0, 6, 1, 5, 2, 16, 13, 14, 10, 3, 27, 6), nrow = 10, byrow = TRUE),
  Rumination_1x3 = matrix(c(6, 1, 9, 0, 7, 1, 7, 2, 9, 6, 8, 5, 6, 16, 0, 14, 0, 3, 1, 6), nrow = 10, byrow = TRUE),
  Behavioural_Symptoms_2x1 = matrix(c(0, 5, 0, 8, 0, 1, 0, 7, 0, 7, 1, 7, 3, 10, 0, 7, 8, 1, 5, 0, 36, 0), nrow = 11, byrow = TRUE),
  Behavioural_Symptoms_2x3 = matrix(c(0, 2, 0, 0, 0, 0, 0, 0, 0, 4, 1, 11, 3, 6, 0, 17, 8, 3, 5, 5, 36, 6), nrow = 11, byrow = TRUE),
  Behavioural_Symptoms_1x3 = matrix(c(5, 2, 8, 0, 1, 0, 7, 0, 7, 4, 7, 11, 10, 6, 7, 17, 1, 3, 0, 5, 0, 6), nrow = 11, byrow = TRUE),
  Anxiety_Personal_Impact_2x1 = matrix(c(0, 5, 0, 5, 0, 1, 0, 7, 0, 10, 0, 7, 0, 1, 2, 6, 2, 6, 9, 5, 8, 0, 32, 0), nrow = 12, byrow = TRUE),
  Anxiety_Personal_Impact_2x3 = matrix(c(0, 0, 0, 0, 0, 0, 0, 1, 0, 4, 0, 6, 0, 0, 2, 8, 2, 15, 9, 10, 8, 3, 32, 7), nrow = 12, byrow = TRUE),
  Anxiety_Personal_Impact_1x3 = matrix(c(5, 0, 5, 0, 1, 0, 7, 1, 10, 4, 7, 6, 1, 0, 6, 8, 6, 15, 5, 10, 0, 3, 0, 7), nrow = 12, byrow = TRUE),
  Attribution_Skepticism_2x1 = matrix(c(0, 6, 0, 12, 0, 12, 0, 14, 0, 8, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 18, 0, 11, 0, 9, 0, 14, 0), nrow = 14, byrow = TRUE),
  Attribution_Skepticism_2x3 = matrix(c(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 14, 0, 8, 0, 14, 1, 1, 0, 17, 18, 0, 11, 0, 9, 0, 14, 0), nrow = 14, byrow = TRUE),
  Attribution_Skepticism_1x3 = matrix(c(6, 0, 12, 0, 12, 0, 14, 0, 8, 0, 1, 14, 0, 8, 0, 14, 0, 1, 0, 17, 0, 0, 0, 0, 0, 0, 0, 0), nrow = 14, byrow = TRUE),
  Impact_Skepticism_2x1 = matrix(c(0, 10, 0, 14, 0, 0, 0, 7, 0, 12, 0, 10, 21, 0, 15, 0, 4, 0, 12, 0, 1, 0), nrow = 11, byrow = TRUE),
  Impact_Skepticism_2x3 = matrix(c(0, 10, 0, 16, 0, 1, 0, 12, 0, 4, 0, 11, 21, 0, 15, 0, 4, 0, 12, 0, 1, 0), nrow = 11, byrow = TRUE),
  Impact_Skepticism_1x3 = matrix(c(10, 10, 14, 16, 0, 1, 7, 12, 12, 4, 10, 11, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0), nrow = 11, byrow = TRUE),
  Trend_Skepticism_2x1 = matrix(c(0, 8, 0, 5, 0, 1, 0, 9, 0, 13, 0, 16, 0, 1, 0, 0, 0, 0, 0, 0, 12, 0, 16, 0, 9, 0, 15, 0, 1, 0), nrow = 15, byrow = TRUE),
  Trend_Skepticism_2x3 = matrix(c(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 14, 0, 18, 0, 13, 0, 9, 12, 0, 16, 0, 9, 0, 15, 0, 1, 0), nrow = 15, byrow = TRUE),
  Trend_Skepticism_1x3 = matrix(c(8, 0, 5, 0, 1, 0, 9, 0, 13, 0, 16, 0, 1, 14, 0, 18, 0, 13, 0, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0), nrow = 15, byrow = TRUE),
  Response_Skepticism_2x1 = matrix(c(0, 0, 1, 0, 16, 13, 11, 12, 16, 13, 9, 14, 0, 1), nrow = 7, byrow = TRUE),
  Response_Skepticism_2x3 = matrix(c(0, 1, 1, 0, 16, 15, 11, 11, 16, 15, 9, 12, 0, 0), nrow = 7, byrow = TRUE),
  Response_Skepticism_1x3 = matrix(c(0, 1, 0, 0, 13, 15, 12, 11, 13, 15, 14, 12, 1, 0), nrow = 7, byrow = TRUE)
)

print("Tabella di Contingenza:")
print(matrici_contingenza)

# Funzione per calcolare i risultati del test esatto di Fisher
calcola_risultati_fisher <- function(matrice) {
  return(fisher.test(matrice, simulate.p.value = TRUE))
}

# Lista per salvare i risultati
risultati_lista <- list()

# Ciclo for per il calcolo del test di Fisher per ogni matrice
for (i in seq_along(matrici_contingenza)) {
  risultati_fisher <- calcola_risultati_fisher(matrici_contingenza[[i]])
  risultati_lista[[i]] <- risultati_fisher
}

# Stampa i risultati
for (i in seq_along(risultati_lista)) {
  cat("Risultati del Test Esatto di Fisher per Matrice", i, ":\n")
  print(risultati_lista[[i]])
  cat("\n")
}

# Salva tutti i risultati nella lista
#saveRDS(risultati_lista, "fisher_test_result.rds")

# Save only the p-values as a separate file
p_values_list <- lapply(risultati_lista, function(result) result$p.value)
names(p_values_list) <- names(risultati_lista)  # Assign names to the list elements
#saveRDS(p_values_list, "fisher_test_p_values.rds")
print(p_values_list)

