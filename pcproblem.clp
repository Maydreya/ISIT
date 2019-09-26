;;****************
;;* DEFFUNCTIONS *
;;****************

(deffunction ask-question (?question $?allowed-values)
   (printout t ?question)
   (bind ?answer (read))
   (if (lexemep ?answer) 
       then (bind ?answer (lowcase ?answer)))
   (while (not (member$ ?answer ?allowed-values)) do
      (printout t ?question)
      (bind ?answer (read))
      (if (lexemep ?answer)
          then (bind ?answer (lowcase ?answer))))
   ?answer)

(deffunction yes-or-no-p (?question)
   (bind ?response (ask-question ?question yes no y n))
   (if (or (eq ?response yes) (eq ?response y))
       then yes 
       else no))

;;;***************
;;;* QUERY RULES *
;;;***************

(defrule determine-video-driver ""
   (not (video-driver ?))
   (not (repair ?))
   =>
   (assert (video-driver (yes-or-no-p "Is there a video driver error (yes/no)? "))))
   
(defrule determine-stripes ""
   (not(stripes ?))
   (video-driver no)
   (not (repair ?))
   =>
   (assert (stripes (yes-or-no-p "Are there any stripes on the screen (yes/no)? "))))
   
(defrule determine-burned-elements ""
   (not(burned-elements ?))
   (not (repair ?))
   =>
   (assert (burned-elements (yes-or-no-p "Are there any burned out elements (yes/no)? "))))
   
(defrule determine-speaker-silent ""
   (not(speaker-silent ?))
   (burned-elements no)
   (not (repair ?))
   =>
   (assert (speaker-silent (yes-or-no-p "Is the speaker silent (yes/no)? "))))
 
(defrule determine-money ""
   (not (repair ?))
   =>
   (assert (money (ask-question "How much can you spend, select price range (10000-30000, 30000-49999, 50000+) ? " 1 2 3))))
   
   (defrule determine-swollen-capacitors ""
   (not(swollen-capacitors ?))
   (speaker-silent no)
   (burned-elements no)
   (not (repair ?))
   =>
   (assert (swollen-capacitors (yes-or-no-p "Are there swollen capacitors (yes/no)? "))))
   
   (defrule determine-bsod ""
   (or(videocard no)
   (motherboard no))
   (not(bsod ?))
   (not (repair ?))
   =>
   (assert (bsod (yes-or-no-p "Is there a BSOD (yes/no)? "))))
   
   (defrule determine-perf-degr ""
   (not(perf-degr ?))
   (bsod no)
   (not (repair ?))
   =>
   (assert (perf-degr (yes-or-no-p "Is there any performance degradation (yes/no)? "))))
   
;;;****************
;;;* dia RULES *
;;;***
   (defrule yes-money-rest-rule ""
   (and(money ?money)
   (test(> ?money 2)))
   =>
   (assert (money-rest yes)))
   
   (defrule no-money-rest-rule ""
   (and(money ?money)
   (test(<= ?money 2)))
   =>
   (assert (money-rest no)))
   
  (defrule videocard ""
  (or(video-driver yes)
		  (stripes yes))
   (not (repair ?))
   =>
   (assert (videocard yes)))
   
   (defrule determine-videocard ""
   (video-driver no)
   (stripes no)
   (not (repair ?))
   =>
   (assert (videocard no)))
   
   (defrule motherboard ""
   (or(burned-elements yes)
	(speaker-silent yes)
	(swollen-capacitors yes))
   (not (repair ?))
   =>
   (assert (motherboard yes)))
   
   (defrule determine-motherboard ""
    (burned-elements no)
	(speaker-silent no)
	(swollen-capacitors no)
   (not (repair ?))
   =>
   (assert (motherboard no)))
   
  (defrule high-cost ""
   (motherboard yes)
   (videocard yes)
   (not (repair ?))
   =>
   (assert (high-cost yes)))
   
   (defrule determine-high-cost ""
   (motherboard no)
   (videocard no)
   (not (repair ?))
   =>
   (assert (high-cost no)))
   
   (defrule hdd ""
   (or(bsod yes)
   (perf-degr yes))
   (not (repair ?))
   =>
   (assert (hdd yes)))
   
   (defrule determine-hdd ""
   (bsod no)
   (perf-degr no)
   (not (repair ?))
   =>
   (assert (hdd no)))
   
   (defrule low-cost ""
   (or(high-cost no)
   (not (high-cost ?)))
   (or(hdd yes)
   (and(motherboard yes)
   (videocard no))
   (and(videocard yes)
   (motherboard no)))
   (not (repair ?))
   =>
   (assert (low-cost yes)))
   
   (defrule determine-low-cost ""
   (high-cost no)
   (hdd no)
   (not (repair ?))
   =>
   (assert (low-cost no)))
   
  (defrule high-costs ""
   (high-cost yes)
   (money-rest yes)
   (not (repair ?))
   =>
   (assert (repair "Buy new pc.")))
   
   (defrule no-problem ""
   (high-cost no)
   (low-cost no)
   (not (repair ?))
   =>
   (assert (repair "No problem.")))
   
   (defrule low-costs ""
   (or(low-cost yes)
   (high-cost yes))
   (money-rest no)
   (not (repair ?))
   =>
   (assert (repair "Replace Accessories.")))
   
   
   ;;;********************************
;;;* STARTUP AND CONCLUSION RULES *
;;;********************************

(defrule system-banner ""
  (declare (salience 10))
  =>
  (printout t crlf crlf)
  (printout t "The PC Diagnosis Expert System")
  (printout t crlf crlf))

(defrule print-repair ""
  (declare (salience 10))
  (repair ?item)
  =>
  (printout t crlf crlf)
  (printout t "Suggested:")
  (printout t crlf crlf)
  (format t " %s%n%n%n" ?item))
